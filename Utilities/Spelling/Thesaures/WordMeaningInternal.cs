namespace NHunspell
{
    /// <summary>
    /// Holds a meaning and its synonyms
    /// </summary>
    internal class WordMeaningInternal
    {
        string description;
        string type;
        string[] synonyms;
        string[] antonyms;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThesMeaning"/> class.
        /// </summary>
        /// <param name="description">The meaning description.</param>
        /// <param name="synonyms">The synonyms for this meaning.</param>
        internal  WordMeaningInternal(string description, string[] synonyms)
        {
            this.description = description;
            this.synonyms = synonyms;
        }

        /// <summary>
        /// Gets the description of the meaning.
        /// </summary>
        /// <value>The description.</value>
        internal string Description { get { return description; } }

        /// <summary>
        /// Gets the synonyms of the meaning.
        /// </summary>
        /// <value>The synonyms.</value>
        internal string[] Synonyms { get { return synonyms; } }
    }
}
