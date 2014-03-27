using System.Collections.Generic;

namespace NHunspell
{
    /// <summary>
    /// Holds a meaning and its synonyms
    /// </summary>
    public class WordMeaning
    {
        string description;
        List<string> synonyms;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThesMeaning"/> class.
        /// </summary>
        /// <param name="description">The meaning description.</param>
        /// <param name="synonyms">The synonyms for this meaning.</param>
        internal  WordMeaning(string description, List<string> synonyms)
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
        internal List<string> Synonyms { get { return synonyms; } }
    }
}
