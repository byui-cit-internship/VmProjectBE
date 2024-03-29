﻿namespace VmProjectBE.DTO.v1
{
    public class CreateVmDTO
    {
        public string Student_name { get; }
        public string Section_name { get; }
        public int Course_id { get; }

        public string Course_semester { get; }
        public int Enrollment_id { get; }
        public string Folder { get; }

        public string Library_Id { get; }

        public CreateVmDTO(string student_name, string section_name, int course_id, string course_semester, int enrollment_id, string folder, string library_id)
        {
            Student_name = student_name;
            Section_name = section_name;
            Course_id = course_id;
            Course_semester = course_semester;
            Enrollment_id = enrollment_id;
            Folder = folder;
            Library_Id = library_id;
        }
    }
}

