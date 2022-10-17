namespace VmProjectBE.DTO.v1
{
    public class SectionDTO
    {
        public string courseName;
        public int sectionId;
        public string semesterTerm;
        public int sectionNumber;
        public string fullName;

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