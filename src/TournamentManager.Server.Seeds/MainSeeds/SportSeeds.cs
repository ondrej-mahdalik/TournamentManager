using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.Seeds.MainSeeds;

public static class SportSeeds
{
    public static readonly SportEntity Football = new("Football")
    {
        Id = Guid.Parse("3F20807D-34FC-49E5-BAE0-AF64D984D7CF")
    };

    public static readonly SportEntity Basketball = new("Basketball")
    {
        Id = Guid.Parse("5BAF064D-95E0-4E94-BD67-DF236CAEC512")
    };

    public static readonly SportEntity IceHockey = new("Ice Hockey")
    {
        Id = Guid.Parse("B9F10E84-77C6-4D4F-82A7-99E124DE3D47")
    };

    public static readonly SportEntity TableTennis = new("TableTennis")
    {
        Id = Guid.Parse("D57407FC-D490-4B50-B1BF-F390A34467FB")
    };

    public static readonly SportEntity Tennis = new("Tennis")
    {
        Id = Guid.Parse("5E01DE8C-0740-4FCF-B20B-9ABEE01B42A3")
    };

    public static readonly SportEntity Chess = new("Chess")
    {
        Id = Guid.Parse("DD8C1414-5230-4182-8C61-B42CD47B3B29")
    };

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Sports.Add(Football);
        dbContext.Sports.Add(Basketball);
        dbContext.Sports.Add(IceHockey);
        dbContext.Sports.Add(TableTennis);
        dbContext.Sports.Add(Tennis);
        dbContext.Sports.Add(Chess);

    }
}