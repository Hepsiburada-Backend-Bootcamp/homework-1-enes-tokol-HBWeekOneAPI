using System.ComponentModel.DataAnnotations;

namespace HBWeekOneAPI.Models
{
    public class Product:IBaseModel
    {
        #region Scalar Properties
        public int Id { get; set; }
        public string Name { get; set; }
        #endregion
    }
}