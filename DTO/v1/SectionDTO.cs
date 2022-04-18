namespace Database_VmProject.Controllers.v1
{
    public class SectionDTO
    {
        private string courseName;
        private int sectionId;
        private string semesterTerm;
        private int sectionNumber;
        private string fullName;

        public SectionDTO(string courseName, int sectionId, string semesterTerm, int sectionNumber, string fullName)
        {
            this.courseName = courseName;
            this.sectionId = sectionId;
            this.semesterTerm = semesterTerm;
            this.sectionNumber = sectionNumber;
            this.fullName = fullName;
        }
    }
}