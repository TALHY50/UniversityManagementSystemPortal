using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystemPortal.Enum
{
    public enum GradeType
    {
        [Display(Name = "Relative Grade")]
        Relative,

        [Display(Name = "Absolute Grade")]
        Absolute
    }
}
