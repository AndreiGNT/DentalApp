using DentalApp.Application.TodoLists.Queries.GetTodos;
using DentalApp.Domain.Entities;
using DentalApp.Domain.ValueObjects;

namespace DentalApp.Application.FunctionalTests.TodoLists.Queries;

using static Testing;

public class GetTodosTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriorityLevels()
    {
        await RunAsDefaultUserAsync();

        var query = new GetTodosQuery();

        var result = await SendAsync(query);

        result.PriorityLevels.ShouldNotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new TodoList
        {
            Title = "Shopping",
            Colour = Colour.Blue,
            Items =
                {
                    new User { Title = "Apples", Done = true },
                    new User { Title = "Milk", Done = true },
                    new User { Title = "Bread", Done = true },
                    new User { Title = "Toilet paper" },
                    new User { Title = "Pasta" },
                    new User { Title = "Tissues" },
                    new User { Title = "Tuna" }
                }
        });

        var query = new GetTodosQuery();

        var result = await SendAsync(query);

        result.Lists.Count.ShouldBe(1);
        result.Lists.First().Items.Count.ShouldBe(7);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetTodosQuery();

        var action = () => SendAsync(query);

        await Should.ThrowAsync<UnauthorizedAccessException>(action);
    }
}
