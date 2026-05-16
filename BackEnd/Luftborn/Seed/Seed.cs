
using System.Text.Json;
using Luftborn.Data;
using Luftborn.Entities;

namespace Luftborn.Seed
{
    public class Seed
    {
        internal static async Task SeedData(DataContext context)
        {
            await ReadData<Category>("Category", context);
            await ReadData<Product>("Product", context);


        }

        private static async Task ReadData<T>(string fileName, DataContext context) where T : class
        {
            if (!context.Set<T>().Any())
            {

                var file = await File.ReadAllTextAsync("seed/" + fileName + ".json");

                var data = JsonSerializer.Deserialize<List<T>>(file);

                await context.Set<T>().AddRangeAsync(data);

                await context.SaveChangesAsync();
            }
        }
    
    
    }
}
