namespace VmProjectBE.DTO.v1
{
    public class CourseListByUserDTO
    {
        public int canvasSectionId { get; set; }
        public int sectionId { get; set; }
        public string sectionName { get; set; }
        public int enrollmentId { get; set; }
        public string studentFullName { get; set; }

        public CourseListByUserDTO(int canvasSectionId, int sectionId, string sectionName, int enrollmentId, string studentFullName)
        {
            this.canvasSectionId = canvasSectionId;
            this.sectionId = sectionId;
            this.sectionName = sectionName;
            this.enrollmentId = enrollmentId;
            this.studentFullName = studentFullName;
        }
    }
}