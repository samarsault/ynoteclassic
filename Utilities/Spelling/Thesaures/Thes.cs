using System.Linq;

namespace NHunspell
{
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;


    /// <summary>
    /// provides thesaurus founctions to get synonyms for a word
    /// </summary>
    public class Thes
    {
        readonly ReaderWriterLockSlim dictionaryLock = new ReaderWriterLockSlim();
        readonly Dictionary<string, WordMeaningInternal[]> synonyms = new Dictionary<string, WordMeaningInternal[]>();
        public Thes()
        {
        }
 
        public void LoadOpenOffice(string datFile ) 





        {
            datFile = Path.GetFullPath(datFile);
            if (!File.Exists(datFile))
                throw new FileNotFoundException("DAT File not found: " + datFile);


            byte[] dictionaryData;
            using (var stream = File.OpenRead(datFile))
            {
                using (var reader = new BinaryReader(stream))
                {
                    dictionaryData = reader.ReadBytes((int)stream.Length);
                }
            }

            LoadOpenOffice(dictionaryData);
        }


        private Encoding GetEncoding(string encoding)
        {
            encoding = encoding.Trim().ToLower();
            switch( encoding )
            {
                case "iso8859-1": return Encoding.GetEncoding(28591);
            }

            return null;
        }

        private int GetLineLength(byte[] buffer, int start)
        {
            for (int i = start; i < buffer.Length; ++i)
            {
                if (buffer[i] == 10 || buffer[i] == 13)
                {
                    return (i - start);
                }
            }
            return ( buffer.Length - start );
        }

        private int GetCrLfLength(byte[] buffer, int pos)
        {
            if (buffer[pos] == 10)
            {
                if (buffer.Length > pos + 1 && buffer[pos] == 13)
                    return 2;
                return 1;
            }
            if (buffer[pos] == 13)
            {
                if (buffer.Length > pos + 1 && buffer[pos] == 10)
                    return 2;
                return 1;
            }
            throw new ArgumentException("buffer[pos] dosen't point to CR or LF");
        }


        public void LoadOpenOffice(byte[] datBytes)
        {
            int currentPos = 0;
            int currentLength = GetLineLength(datBytes, currentPos);

            string fileEncoding = Encoding.ASCII.GetString(datBytes, currentPos, currentLength);
            Encoding enc = GetEncoding(fileEncoding);
            currentPos += currentLength;

            bool wordLine = true;
            int synonymLines = 0;
            string word = null;
            IList<WordMeaningInternal> meanings = new List<WordMeaningInternal>();
            
            while (currentPos < datBytes.Length)
            {
                currentPos += GetCrLfLength(datBytes, currentPos);
                currentLength = GetLineLength(datBytes, currentPos);
                string lineText = enc.GetString(datBytes, currentPos, currentLength).Trim();

                if (lineText != null && lineText.Length > 0)
                {
                    string[] tokens = lineText.Split('|');
                    if (wordLine)
                    {
                        word = tokens[0];
                        synonymLines = int.Parse(tokens[1]);
                        wordLine = false;
                    }
                    else
                    {

                        List<string> synonyms = new List<string>();
                        string description = null;
                        for (int tokIndex = 1; tokIndex < tokens.Length; ++tokIndex)
                        {
                            synonyms.Add(tokens[tokIndex]);
                            if (tokIndex == 1)
                            {
                                description = tokens[tokIndex];
                            }

                        }
                        var meaning = new WordMeaningInternal(description, synonyms.ToArray());
                        meanings.Add(meaning);
                        --synonymLines;
                        if (synonymLines <= 0)
                        {
                            wordLine = true;

                            dictionaryLock.EnterWriteLock();
                            try
                            {
                                this.synonyms.Add(word, meanings.ToArray());
                            }
                            finally
                            {
                                dictionaryLock.ExitWriteLock();
                            }
                            meanings = new List<WordMeaningInternal>();
                        }


                    }
                }

                currentPos += currentLength;
            }
            
            
        }
    
    
    }
}

