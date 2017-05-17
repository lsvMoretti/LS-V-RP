using System.ComponentModel.DataAnnotations;

namespace Roleplay.Character
{
    public class Characters
    {
        [Key]
        public int ID { get; set; }

        public int AID { get; set; }

        public string Name { get; set; }

        public string Admin { get; set; }

        public string RegisterDate { get; set; }

        public int Model { get; set; }

        public string ModelName { get; set; }

        public int RegistrationStep { get; set; }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float Rot { get; set; }

        public int Cash { get; set; }

        public int Bank { get; set; }
    }
}
