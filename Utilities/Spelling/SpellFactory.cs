// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpellFactory.cs" company="Maierhofer Software and the Hunspell Developers">
//   (c) by Maierhofer Software an the Hunspell Developers
// </copyright>
// <summary>
//   Enables spell checking, hyphenation and thesaurus based synonym lookup in a thread safe manner.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NHunspell
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Enables spell checking, hyphenation and thesaurus based synonym lookup in a thread safe manner.
    /// </summary>
    public class SpellFactory : IDisposable
    {
        #region Constants and Fields

        /// <summary>
        /// The processors.
        /// </summary>
        private readonly int processors;

        /// <summary>
        /// The disposed.
        /// </summary>
        private volatile bool disposed;

        /// <summary>
        /// The hunspell semaphore.
        /// </summary>
        private Semaphore hunspellSemaphore;

        /// <summary>
        /// The hunspells.
        /// </summary>
        private Stack<Hunspell> hunspells;

        /// <summary>
        /// The hyphen semaphore.
        /// </summary>
        private Semaphore hyphenSemaphore;

        /// <summary>
        /// The hyphens.
        /// </summary>
        private Stack<Hyphen> hyphens;

        /// <summary>
        /// The my thes semaphore.
        /// </summary>
        private Semaphore myThesSemaphore;

        /// <summary>
        /// The my theses.
        /// </summary>
        private Stack<MyThes> myTheses;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellFactory"/> class.
        /// </summary>
        /// <param name="config">
        /// The configuration of the language.
        /// </param>
        public SpellFactory(LanguageConfig config)
        {
            this.processors = config.Processors;

            if (config.HunspellAffFile != null && config.HunspellAffFile != string.Empty)
            {
                this.hunspells = new Stack<Hunspell>(this.processors);
                for (int count = 0; count < this.processors; ++count)
                {
                    if (config.HunspellKey != null && config.HunspellKey != string.Empty)
                    {
                        this.hunspells.Push(
                            new Hunspell(config.HunspellAffFile, config.HunspellDictFile, config.HunspellKey));
                    }
                    else
                    {
                        this.hunspells.Push(new Hunspell(config.HunspellAffFile, config.HunspellDictFile));
                    }
                }
            }

            if (config.HyphenDictFile != null && config.HyphenDictFile != string.Empty)
            {
                this.hyphens = new Stack<Hyphen>(this.processors);
                for (int count = 0; count < this.processors; ++count)
                {
                    this.hyphens.Push(new Hyphen(config.HyphenDictFile));
                }
            }

            if (config.MyThesIdxFile != null && config.MyThesIdxFile != string.Empty)
            {
                this.myTheses = new Stack<MyThes>(this.processors);
                for (int count = 0; count < this.processors; ++count)
                {
                    this.myTheses.Push(new MyThes(config.MyThesIdxFile, config.MyThesDatFile));
                }
            }

            this.hunspellSemaphore = new Semaphore(this.processors, this.processors);
            this.hyphenSemaphore = new Semaphore(this.processors, this.processors);
            this.myThesSemaphore = new Semaphore(this.processors, this.processors);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisposed
        {
            get
            {
                return this.disposed;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Analyzes the specified word.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// List of strings with the morphology. One string per word stem
        /// </returns>
        public List<string> Analyze(string word)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException("SpellFactory");
            }

            if (this.hunspells == null)
            {
                throw new InvalidOperationException("Hunspell Dictionary isn't loaded");
            }

            this.hunspellSemaphore.WaitOne();
            Hunspell current = null;
            try
            {
                current = this.hunspells.Pop();
                return current.Analyze(word);
            }
            finally
            {
                if (current != null)
                {
                    this.hunspells.Push(current);
                }

                this.hunspellSemaphore.Release();
            }
        }

        /// <summary>
        /// Generates the specified word by example.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <param name="sample">
        /// The sample.
        /// </param>
        /// <returns>
        /// List of generated words, one per word stem
        /// </returns>
        public List<string> Generate(string word, string sample)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException("SpellFactory");
            }

            if (this.hunspells == null)
            {
                throw new InvalidOperationException("Hunspell Dictionary isn't loaded");
            }

            this.hunspellSemaphore.WaitOne();
            Hunspell current = null;
            try
            {
                current = this.hunspells.Pop();
                return current.Generate(word, sample);
            }
            finally
            {
                if (current != null)
                {
                    this.hunspells.Push(current);
                }

                this.hunspellSemaphore.Release();
            }
        }

        /// <summary>
        /// Hyphenates the specified word.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// the result of the hyphenation
        /// </returns>
        public HyphenResult Hyphenate(string word)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException("SpellFactory");
            }

            if (this.hunspells == null)
            {
                throw new InvalidOperationException("Hyphen Dictionary isn't loaded");
            }

            this.hyphenSemaphore.WaitOne();
            Hyphen current = null;
            try
            {
                current = this.hyphens.Pop();
                return current.Hyphenate(word);
            }
            finally
            {
                if (current != null)
                {
                    this.hyphens.Push(current);
                }

                this.hyphenSemaphore.Release();
            }
        }

        /// <summary>
        /// Look up the synonyms for the word.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <param name="useGeneration">
        /// if set to <c>true</c> use generation to get synonyms over the word stem.
        /// </param>
        /// <returns>
        /// </returns>
        public ThesResult LookupSynonyms(string word, bool useGeneration)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException("SpellFactory");
            }

            if (this.myTheses == null)
            {
                throw new InvalidOperationException("MyThes Dictionary isn't loaded");
            }

            if (useGeneration && this.hunspells == null)
            {
                throw new InvalidOperationException("Hunspell Dictionary isn't loaded");
            }

            MyThes currentThes = null;
            Hunspell currentHunspell = null;
            this.myThesSemaphore.WaitOne();
            if (useGeneration)
            {
                this.hunspellSemaphore.WaitOne();
            }

            try
            {
                currentThes = this.myTheses.Pop();
                if (useGeneration)
                {
                    currentHunspell = this.hunspells.Pop();
                    return currentThes.Lookup(word, currentHunspell);
                }

                return currentThes.Lookup(word);
            }
            finally
            {
                if (currentThes != null)
                {
                    this.myTheses.Push(currentThes);
                }

                if (currentHunspell != null)
                {
                    this.hunspells.Push(currentHunspell);
                }

                this.myThesSemaphore.Release();
                if (useGeneration)
                {
                    this.hunspellSemaphore.Release();
                }
            }
        }

        /// <summary>
        /// Spell check the specified word.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// true if word is correct, otherwise false. 
        /// </returns>
        public bool Spell(string word)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException("SpellFactory");
            }

            if (this.hunspells == null)
            {
                throw new InvalidOperationException("Hunspell Dictionary isn't loaded");
            }

            this.hunspellSemaphore.WaitOne();
            Hunspell current = null;
            try
            {
                current = this.hunspells.Pop();
                return current.Spell(word);
            }
            finally
            {
                if (current != null)
                {
                    this.hunspells.Push(current);
                }

                this.hunspellSemaphore.Release();
            }
        }

        /// <summary>
        /// Stems the specified word.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// list of word stems
        /// </returns>
        public List<string> Stem(string word)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException("SpellFactory");
            }

            if (this.hunspells == null)
            {
                throw new InvalidOperationException("Hunspell Dictionary isn't loaded");
            }

            this.hunspellSemaphore.WaitOne();
            Hunspell current = null;
            try
            {
                current = this.hunspells.Pop();
                return current.Stem(word);
            }
            finally
            {
                if (current != null)
                {
                    this.hunspells.Push(current);
                }

                this.hunspellSemaphore.Release();
            }
        }

        /// <summary>
        /// The suggest.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public List<string> Suggest(string word)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException("SpellFactory");
            }

            if (this.hunspells == null)
            {
                throw new InvalidOperationException("Hunspell Dictionary isn't loaded");
            }

            this.hunspellSemaphore.WaitOne();
            Hunspell current = null;
            try
            {
                current = this.hunspells.Pop();
                return current.Suggest(word);
            }
            finally
            {
                if (current != null)
                {
                    this.hunspells.Push(current);
                }

                this.hunspellSemaphore.Release();
            }
        }

        #endregion

        #region Implemented Interfaces

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Semaphore currentHunspellSemaphore = this.hunspellSemaphore;

            if (!this.IsDisposed)
            {
                this.disposed = true; // Alle Threads werden ab jetzt mit Disposed Exception abgewiesen

                for (int semaphoreCount = 0; semaphoreCount < this.processors; ++ semaphoreCount)
                {
                    this.hunspellSemaphore.WaitOne();

                    // Complete Ownership will be taken, to guarrantee other threads are completed
                }

                if (this.hunspells != null)
                {
                    foreach (Hunspell hunspell in this.hunspells)
                    {
                        hunspell.Dispose();
                    }
                }

                this.hunspellSemaphore.Release(this.processors);
                this.hunspellSemaphore = null;
                this.hunspells = null;

                for (int semaphoreCount = 0; semaphoreCount < this.processors; ++semaphoreCount)
                {
                    this.hyphenSemaphore.WaitOne();

                    // Complete Ownership will be taken, to guarrantee other threads are completed
                }

                if (this.hyphens != null)
                {
                    foreach (Hyphen hyphen in this.hyphens)
                    {
                        hyphen.Dispose();
                    }
                }

                this.hyphenSemaphore.Release(this.processors);
                this.hyphenSemaphore = null;
                this.hyphens = null;

                for (int semaphoreCount = 0; semaphoreCount < this.processors; ++semaphoreCount)
                {
                    this.myThesSemaphore.WaitOne();

                    // Complete Ownership will be taken, to guarrantee other threads are completed
                }

                if (this.myTheses != null)
                {
                    foreach (MyThes myThes in this.myTheses)
                    {
                        // myThes.Dispose();
                    }
                }

                this.myThesSemaphore.Release(this.processors);
                this.myThesSemaphore = null;
                this.myTheses = null;
            }
        }

        #endregion

        #endregion
    }
}