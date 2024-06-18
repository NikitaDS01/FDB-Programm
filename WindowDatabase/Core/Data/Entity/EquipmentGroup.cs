using FileDB.Core.Attribute;
using FileDB.Core.Data;

public class EquipmentGroup
{
    [SerializeIndex]
    public int Index { get; set; }

    public RecordLink EquipmentGEN { get; set; }
    public RecordLink EquipmentIN { get; set; }
    public RecordLink EquipmentTEL { get; set; }

    public EquipmentGroup(int indexIn, RecordLink equipment1,
        RecordLink equipment2, RecordLink equipment3)
    {
        EquipmentGEN = equipment1;
        EquipmentIN = equipment2;
        EquipmentTEL = equipment3;
        Index = indexIn;
    }
    public EquipmentGroup()
    {
        EquipmentGEN = RecordLink.Empty;
        EquipmentIN = RecordLink.Empty;
        EquipmentTEL = RecordLink.Empty;
        Index = 0;
    }
}
