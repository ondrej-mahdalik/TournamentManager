using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.Seeds.MainSeeds;

public static class TeamSeeds
{
    public static readonly TeamEntity JohnsTeam = new(
            "John's team", "https://i.ytimg.com/vi/QQNL83fhWJU/maxresdefault.jpg")
    {
        Id = Guid.Parse("B7327A1C-DECE-4251-9467-F6A87D74E349"),
        LeaderId = UserSeeds.JohnDoe.Id,
    };

    public static readonly TeamEntity Team1 = new(
        "UTI", null)
    {
        Id = Guid.Parse("6C26A9A3-2A53-47F1-8AA9-87846AD2C3B6"),
        LeaderId = UserSeeds.Admin.Id
    };

    public static readonly TeamEntity Team2 = new(
        "TTTT", null)
    {
        Id = Guid.Parse("6B26A9A4-2A57-47F1-8AA9-87846AD2C3B6"),
        LeaderId = UserSeeds.Admin.Id
    };

    public static readonly TeamEntity Team3 = new(
        "Team3", null)
    {
        Id = Guid.Parse("5526A9A5-2A53-47F1-8BB9-87846AD2C366"),
        LeaderId = UserSeeds.Admin.Id
    };

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        JohnsTeam.Members.Add(UserIsInTeamSeeds.JohnInJohns);
        dbContext.Teams.Add(JohnsTeam);
        dbContext.Teams.Add(Team1);
        dbContext.Teams.Add(Team2);
        dbContext.Teams.Add(Team3);
    }
}