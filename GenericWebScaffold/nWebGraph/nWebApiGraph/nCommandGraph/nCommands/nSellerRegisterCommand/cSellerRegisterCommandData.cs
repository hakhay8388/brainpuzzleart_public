using System;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSellerRegisterCommand
{
    public class cSellerRegisterCommandData
    {
        public string Email;
        public string Name;
        public string Surname;
        public string Password;
		public string PasswordConfirm;
		public DateTime DateOfBirth;
        public string Telephone;
        public bool IsSeller;
        public int? EducationLevel;
        public long? UniversitySection;
        public long? University;
        public string OtherUniversity;
        public string OtherSection;
        public bool OtherUniSelected;
    }
}