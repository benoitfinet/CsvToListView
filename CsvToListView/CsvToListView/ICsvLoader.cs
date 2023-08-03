using System.Collections.Generic;

namespace CsvToListView
{
    public interface ICsvLoader
    {
        List<Properties> LoadData();
    }
}
