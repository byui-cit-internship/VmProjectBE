using VmProjectBE.Models;

namespace VmProjectBE.DTO
{
    public class UserVmInstance
    {
        public User User { get; set; }
        public VmInstance VmInstance { get; set; }

        public UserVmInstance(User user, VmInstance vmInstance)
        {
            User = user;
            VmInstance = vmInstance;
        }
    }
}
