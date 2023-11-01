using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.InsPolicy.Travel
{
    public class TravelClassZoom
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int ZoomId { get; set; }
        #region Relations
        [ForeignKey(nameof(ClassId))]
        public TravelInsClass TravelInsClass { get; set; }
        [ForeignKey(nameof(ZoomId))]
        public TravelZoom TravelZoom { get; set; }
        #endregion
    }
}
