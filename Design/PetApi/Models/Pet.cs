using System.Xml;
namespace PetApi.Models
{
    /// <summary>
    /// Pet information
    /// </summary>
    public class Pet
    {
        /// <summary>
        /// Pet internat identification number
        /// </summary>
        /// <example>1234</example>
        public int Id { get; set; }

        /// <summary>
        /// Pet international tag
        /// </summary>
        /// <example>XYZ-123</example>
        public string Tag { get; set; }

        /// <summary>
        /// Pets name
        /// </summary>
        /// <example>Doggy</example>
        public string Name { get; set; }
    }
}