namespace VmProjectBE.DTO.v1
{
    public class SectionDTO
    {
        public string courseCode;
        public string sectionName;
        public int sectionId;
        public string semesterTerm;
        public int sectionNumber;
        public string fullName;

        public SectionDTO(string courseCode, string sectionName, int sectionId, string semesterTerm, int sectionNumber, string fullName)
        {
            this.courseCode = courseCode;
            this.sectionName = sectionName;
            this.sectionId = sectionId;
            this.semesterTerm = semesterTerm;
            this.sectionNumber = sectionNumber;
            this.fullName = fullName;
        }
    }
}