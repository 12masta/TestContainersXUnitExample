using System.Threading.Tasks;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Configurations.Databases;
using DotNet.Testcontainers.Containers.Modules.Databases;
using Xunit;
using System.Data.SqlClient;


namespace TestContainersXUnitExample
{
    public class UnitTest1
    {
        public UnitTest1()
        {
        }

        [Fact]
        public async Task InitContainerTest()
        {
            // Given
            var testcontainersBuilder = new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithDatabase(new MsSqlTestcontainerConfiguration
                {
                    Password = "yourStrong(!)Password123",
                });

            // When
            // Then
            await using (var testcontainer = testcontainersBuilder.Build())
            {
                await testcontainer.StartAsync();

                await using (var connection = new SqlConnection(testcontainer.ConnectionString))
                {
                    connection.Open();

                    await using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT 1";
                        cmd.ExecuteReader();
                    }
                }
            }
        }
    }
}