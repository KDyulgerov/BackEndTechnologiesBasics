namespace TownApplication.IntegrationTests
{
    public class TownControllerIntegrationTests
    {
        private readonly TownController _controller;

        public TownControllerIntegrationTests()
        {
            _controller = new TownController();
            _controller.ResetDatabase();
        }

        [Fact]
        public void AddTown_ValidInput_ShouldAddTown()
        {
            var validTownName = "Lisbon";
            var population = 1000;

            _controller.AddTown(validTownName, population);

            var result = _controller.GetTownByName(validTownName);

            Assert.NotNull(result);
            Assert.StrictEqual(population, result.Population);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("AB")]
        public void AddTown_InvalidName_ShouldThrowArgumentException(string invalidName)
        {
            var population = 2000;

            var exception = Assert.Throws<ArgumentException>(() =>  _controller.AddTown(invalidName, population));
            Assert.Equal("Invalid town name.", exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void AddTown_InvalidPopulation_ShouldThrowArgumentException(int invalidPopulation)
        {
            var validTownName = "Porto";

            var exception = Assert.Throws<ArgumentException>(() => _controller.AddTown(validTownName, invalidPopulation));

            Assert.Equal("Population must be a positive number.", exception.Message);
        }

        [Fact]
        public void AddTown_DuplicateTownName_DoesNotAddDuplicateTown()
        {
            var validTownName = "Lisbon";
            var duplicateTownName = validTownName;
            var population = 100;
 
            _controller.AddTown(validTownName, population);
            
            _controller.AddTown(duplicateTownName, population + 100);

            var result = _controller.ListTowns();
            Assert.Single(result);
            var item = result[0];
            Assert.Equal(population, item.Population);
            Assert.Equal(validTownName, item.Name);
        }

        [Fact]
        public void UpdateTown_ShouldUpdatePopulation()
        {
            var townName = "Barcelona";
            var initalPopulation = 10000;
            var updatedPopulation = 100;

            _controller.AddTown(townName, initalPopulation);

            var town = _controller.GetTownByName(townName);
            _controller.UpdateTown(town.Id, updatedPopulation);

            var updatedTown = _controller.GetTownByName(townName);

            Assert.NotNull(updatedTown);
            Assert.Equal(updatedPopulation, updatedTown.Population);
        }

        [Fact]
        public void DeleteTown_ShouldDeleteTown()
        {
            var townName = "Sofia";
            var population = 500;

            _controller.AddTown(townName, population);

            var townForDeletion = _controller.GetTownByName(townName);
            _controller.DeleteTown(townForDeletion.Id);

            var result = _controller.GetTownByName(townName);
            Assert.Null(result);
        }

        [Fact]
        public void ListTowns_ShouldReturnTowns()
        {
            var townsToAdd = new List<string> { "Sofia", "Plovdiv", "Varna", "Veliko Tarnovo", "Stara Zagora" };

            foreach (var town in townsToAdd)
            {
                _controller.AddTown(town, town.Length * 1000);
            }
            var allTowns = _controller.ListTowns();


            Assert.Equal(townsToAdd.Count, allTowns.Count);
            
            foreach(var town in townsToAdd)
            {
                var existingTown = allTowns.FirstOrDefault(x => x.Name == town);
                Assert.NotNull(existingTown);
                Assert.Equal(town.Length * 1000, existingTown.Population);
            }
        }
    }
}
