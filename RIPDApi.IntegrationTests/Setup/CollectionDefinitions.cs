namespace RIPDApi.IntegrationTests;

[CollectionDefinition("WithUser")]
public class UserTestsCollection : ICollectionFixture<UserFixture>
{
}

[CollectionDefinition("Food", DisableParallelization = true)]
public class FoodTestsCollection : ICollectionFixture<UserFixture>
{
}