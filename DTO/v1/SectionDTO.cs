namespace VmProjectBE.DTO.v1
{
    public class SectionDTO
    {
        public string courseCode;
        public int courseId;
        public int folderId;
        public string sectionName;
        public int sectionId;
        public string semesterTerm;
        public int semesterId;
        public int sectionNumber;
        public string fullName;
        public string LibraryVCenterId;
        public int resourcePoolId;
        public int sectionCanvasId;
        public int sectionRoleId;
        public int vmCount;

        

        public SectionDTO(string courseCode, int courseId, int folderId, string sectionName, int sectionId, string semesterTerm, int semesterId, int sectionNumber, string fullName, string LibraryVCenterId, int resourcePoolId, int sectionCanvasId, int sectionRoleId, int vmCount=0)
        {
            this.courseCode = courseCode;
            this.courseId = courseId;
            this.folderId = folderId;
            this.sectionName = sectionName;
            this.sectionId = sectionId;
            this.semesterTerm = semesterTerm;
            this.semesterId = semesterId;
            this.sectionNumber = sectionNumber;
            this.fullName = fullName;
            this.LibraryVCenterId = LibraryVCenterId;
            this.resourcePoolId = resourcePoolId;
            this.sectionCanvasId = sectionCanvasId;
            this.sectionRoleId = sectionRoleId;
            this.vmCount = vmCount;
        }
    }
}