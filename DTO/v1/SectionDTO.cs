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
        public string LibraryVCenterId;

        public int vmCount;

        public SectionDTO(string courseCode, string sectionName, int sectionId, string semesterTerm, int sectionNumber, string fullName, string LibraryVCenterId, int vmCount=0)
        {
            this.courseCode = courseCode;
            this.sectionName = sectionName;
            this.sectionId = sectionId;
            this.semesterTerm = semesterTerm;
            this.sectionNumber = sectionNumber;
            this.fullName = fullName;
            this.LibraryVCenterId = LibraryVCenterId;
            this.vmCount = vmCount;
        }
    }
}