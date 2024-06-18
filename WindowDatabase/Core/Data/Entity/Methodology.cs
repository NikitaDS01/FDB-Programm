using FileDB.Core.Attribute;
using System.Xml.Linq;

namespace WindowDatabase.Core.Data.Entity
{
    public class Methodology
    {
        [SerializeIndex, SerializeNameRecord(Name="Имя")]
        public string Name { get; set; }
        [SerializeNameRecord(Name = "Описание генераторной установки")]
        public string DescriptionGenerator { get; set; }
        [SerializeNameRecord(Name = "Описание измерительной установки")]
        public string DescriptionMeasuring { get; set; }
        [SerializeNameRecord(Name = "Описание телеметрической установки")]
        public string DescriptionTelemetry { get; set; }
        [SerializeNameRecord(Name = "Описание режимов работы аппаратуры")]
        public string DescriptionModes { get; set; }
        public Methodology(string name, string descriptionGenerator, string descriptionMeasuring, string descriptionTelemetry, string descriptionModes)
        {
            Name = name;
            DescriptionGenerator = descriptionGenerator;
            DescriptionMeasuring = descriptionMeasuring;
            DescriptionTelemetry = descriptionTelemetry;
            DescriptionModes = descriptionModes;
        }
        public Methodology()
        {
            Name = string.Empty;
            DescriptionGenerator = string.Empty;
            DescriptionMeasuring = string.Empty;
            DescriptionTelemetry = string.Empty;
            DescriptionModes = string.Empty;
        }
    }
}
