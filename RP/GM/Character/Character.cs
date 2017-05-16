using System.ComponentModel.DataAnnotations;

namespace Roleplay.Character
{
    public class Characters
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Admin { get; set; }

        public string RegisterDate { get; set; }

        public string Model { get; set; }

        public string ModelName { get; set; }
    }
}
