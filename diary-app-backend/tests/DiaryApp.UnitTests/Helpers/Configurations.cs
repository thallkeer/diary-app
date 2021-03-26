using AutoFixture;

namespace DiaryApp.UnitTests.Helpers
{
    public static class Configurations
    {
        public static IFixture GetFixture()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            //fixture.Customize(new PageAreaCustomization());
            //fixture.Customize(new PageCustomization());
            return fixture;
        }
    }
}
