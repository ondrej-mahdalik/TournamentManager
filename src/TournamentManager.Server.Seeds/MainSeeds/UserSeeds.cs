using System.Globalization;
using TournamentManager.Server.Auth.Seeds;
using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.Seeds.MainSeeds;

public static class UserSeeds
{
    public static readonly UserEntity Admin = new(
        DateTime.Now,
        ApplicationUserSeeds.Admin.Id,
        true)
    {
        Id = Guid.Parse("13F7D8CC-EB4D-4F32-A990-4444BC677A80")
    };
    
    public static readonly UserEntity JohnDoe = new(
        DateTime.Now - TimeSpan.FromDays(1),
        ApplicationUserSeeds.User1.Id)
    {
        Id = Guid.Parse("817A00B4-6A3A-427E-B569-1C72342483A3")
    };

    public static readonly UserEntity RexWalsh = new(
        DateTime.Parse("12/22/2020 15:21", CultureInfo.InvariantCulture),
        ApplicationUserSeeds.RexWalsh.Id)
    {
        Id = Guid.Parse("70969DD5-5074-461F-8540-3F7F75E7F79F")
    };

    public static readonly UserEntity AlenaVaf = new(
        DateTime.Parse("2/2/2020 04:45", CultureInfo.InvariantCulture),
        ApplicationUserSeeds.AlenaVaf.Id)
    {
        Id = Guid.Parse("C98561BA-A899-41A8-82D0-9A11332CC3D2")
    };

    public static readonly UserEntity PavelWolf = new(
        DateTime.Parse("4/2/2021 6:32", CultureInfo.InvariantCulture),
        ApplicationUserSeeds.PavelWolf.Id)
    {
        Id = Guid.Parse("A2132D0D-458C-4D55-BC07-C3E997A1830D")
    };

    public static readonly UserEntity DarthVader = new(
        DateTime.Parse("3/28/1977", CultureInfo.InvariantCulture),
        ApplicationUserSeeds.DarthVader.Id)
    {
        Id = Guid.Parse("C7D583E3-9CE7-4F86-AF22-427DC3B64E8F")
    };
    // TODO User seeds

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Users.Add(JohnDoe);
        dbContext.Users.Add(Admin);
        dbContext.Users.Add(RexWalsh);
        dbContext.Users.Add(AlenaVaf);
        dbContext.Users.Add(PavelWolf);
        dbContext.Users.Add(DarthVader);
    }
}