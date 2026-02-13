using API.Data;
using API.DTOs.Team;
using API.DTOs.UserPokemon;
using API.Models.User;
using API.Repository;
using FluentAssertions;


namespace APITests.Repository;

public class TeamRepositoryTests
{
    private readonly ApplicationDbContext _testDbContext;
    
    public TeamRepositoryTests()
    {
        _testDbContext = Utility.CreateTestDbContext();
    }

    const string TestUser1Id = "test1";
    const string TestUser2Id = "test2";




    [Fact]
    public void GetTeams_WithoutTeamsMatchingUserId_ReturnsEmptyList()
    {
        // Arrange
        var expectedResult = new List<Team>();

        var testTeam = new Team()
        {
            AppUserId = TestUser1Id,
            TeamName = "TestTeam"
        };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();
        

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.GetTeams(TestUser2Id);


        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Fact]
    public void GetTeams_WithTeamsMatchingUserId_ReturnsListOfTeams()
    {
        // Arrange
        var testTeams = new List<Team>()
        {
            new()
            {
                AppUserId = TestUser1Id,
                TeamName = "TestTeam"
            }
        };

        _testDbContext.Team.AddRange(testTeams);
        _testDbContext.SaveChanges();
        
        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.GetTeams(TestUser1Id);


        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(testTeams.Count);
        result.All(x => x.AppUserId == TestUser1Id).Should().BeTrue();
    }


    [Fact]
    public void GetTeams_WithoutTeamsInDb_ReturnsEmptyListOfTeams()
    {
        // Arrange
        var expectedResult = new List<Team>();
        
        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.GetTeams(TestUser1Id);


        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);
    }




    [Fact]
    public void GetTeamById_WithMatchingId_ReturnsTeam()
    {
        // Arrange
        var testTeam = new Team()
        {
            AppUserId = TestUser1Id,
            TeamName = "TestTeam"
        };
        
        var testUserPokemon = new UserPokemon()
        {
            TeamId = testTeam.Id,
            Nickname = "test"
        };

        testTeam.UserPokemon = new List<UserPokemon>() { testUserPokemon };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.GetTeamById(testTeam.Id, TestUser1Id);
        var resultUserPokemon = result?.UserPokemon.FirstOrDefault();


        // Assert
        result.Should().NotBeNull();
        Assert.Equal(testTeam.TeamName, result?.TeamName);
        resultUserPokemon.Should().NotBeNull();
        Assert.Equal(testUserPokemon.Nickname, resultUserPokemon?.Nickname);
    }


    [Fact]
    public void GetTeamById_WithoutMatchingId_ReturnsNull()
    {
        // Arrange
        const int TestId = -1;

        var testTeam = new Team()
        {
            AppUserId = TestUser1Id,
            TeamName = "TestTeam"
        };
    
        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.GetTeamById(TestId, TestUser1Id);


        // Assert
        result.Should().BeNull();
    }


    [Fact]
    public void GetTeamById_WithoutMatchingUserId_ReturnsNull()
    {
        // Arrange
        var testTeam = new Team()
        {
            AppUserId = TestUser1Id,
            TeamName = "TestTeam"
        };
    
        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.GetTeamById(testTeam.Id, TestUser2Id);


        // Assert
        result.Should().BeNull();
    }




    [Fact]
    public void CreateTeam_WithValidData_ReturnsTeam()
    {
        // Arrange
        CreateUpdateTeamDTO createTeamDTO = new()
        {
            TeamName = "TestTeam"
        };

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.CreateTeam(createTeamDTO, TestUser1Id);


        // Assert
        result.Should().NotBeNull();
        result.AppUserId.Should().Be(TestUser1Id);
        result.TeamName.Should().Be(createTeamDTO.TeamName);
    }


    [Fact]
    public void CreateTeam_WithValidData_AddsTeamToDb()
    {
        // Arrange
        CreateUpdateTeamDTO createTeamDTO = new()
        {
            TeamName = "TestTeam"
        };

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.CreateTeam(createTeamDTO, TestUser1Id);
        var dbQueryResult = _testDbContext.Team.FirstOrDefault(x => x.TeamName == createTeamDTO.TeamName);


        // Assert
        result.Should().NotBeNull();
        dbQueryResult.Should().NotBeNull();
        Assert.Equal(TestUser1Id, dbQueryResult?.AppUserId);
        Assert.Equal(createTeamDTO.TeamName, dbQueryResult?.TeamName);
    }


    [Fact]
    public void CreateTeam_WithTeamIncludingUserPokemon_AddsUserPokemonToDb()
    {
        // Arrange
        CreateUpdateTeamDTO createTeamDTO = new()
        {
            TeamName = "TestTeam",
            UserPokemon = new()
        };

        CreateUserPokemonDTO createUserPokemonDTO = new()
        {
            Nickname = "test",
        };

        createTeamDTO.UserPokemon.Add(createUserPokemonDTO);


        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.CreateTeam(createTeamDTO, TestUser1Id);
        var dbTeam = _testDbContext.Team.FirstOrDefault(x => x.TeamName == createTeamDTO.TeamName);
        var dbUserPokemon = _testDbContext.UserPokemon.FirstOrDefault(x => x.Nickname == createUserPokemonDTO.Nickname);


        // Assert
        result.Should().NotBeNull();
        dbTeam.Should().NotBeNull();
        dbUserPokemon.Should().NotBeNull();        
    }




    [Fact]
    public void UpdateTeamById_WithValidInput_ReturnsTeam()
    {
        // Arrange
        const string OriginalTeamName = "TestTeam";
        const string NewTeamName = "UpdatedTestTeam";

        Team testTeam = new()
        {
            AppUserId = TestUser1Id,
            TeamName = OriginalTeamName
        };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();


        CreateUpdateTeamDTO updateTeamDTO = new()
        {
            TeamName = NewTeamName
        };

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.UpdateTeamById(testTeam.Id, updateTeamDTO, TestUser1Id);


        // Assert
        result.Should().NotBeNull();
        Assert.Equal(NewTeamName, result?.TeamName);
    }


    [Fact]
    public void UpdateTeamById_WithValidInput_UpdatesTeamInDb()
    {
        // Arrange
        const string OriginalTeamName = "TestTeam";
        const string NewTeamName = "UpdatedTestTeam";

        Team testTeam = new()
        {
            AppUserId = TestUser1Id,
            TeamName = OriginalTeamName
        };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();


        CreateUpdateTeamDTO updateTeamDTO = new()
        {
            TeamName = NewTeamName
        };

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.UpdateTeamById(testTeam.Id, updateTeamDTO, TestUser1Id);
        var oldTeamNameQueryResult = _testDbContext.Team.FirstOrDefault(x => x.TeamName == OriginalTeamName);
        var newTeamNameQueryResult = _testDbContext.Team.FirstOrDefault(x => x.TeamName == NewTeamName);


        // Assert
        result.Should().NotBeNull();
        oldTeamNameQueryResult.Should().BeNull();
        newTeamNameQueryResult.Should().NotBeNull();
        Assert.Equal(NewTeamName, newTeamNameQueryResult?.TeamName);
    }


    [Fact]
    public void UpdateTeamById_WithUserPokemon_ReplacesUserPokemonDb()
    {
        // Arrange
        const string OriginalNickname = "original";
        const string NewNickname = "updated";

        Team testTeam = new()
        {
            AppUserId = TestUser1Id,
        };

        UserPokemon testUserPokemon = new()
        {
            TeamId = testTeam.Id,
            Nickname = OriginalNickname
        };

        testTeam.UserPokemon = new() { testUserPokemon };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();

        CreateUserPokemonDTO createUserPokemonDTO = new()
        {
            Nickname = NewNickname
        };

        CreateUpdateTeamDTO updateTeamDTO = new()
        {
            TeamName = "UpdatedTestTeam",
            UserPokemon = new() { createUserPokemonDTO }
        };

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.UpdateTeamById(testTeam.Id, updateTeamDTO, TestUser1Id);
        var oldNicknameQueryResult = _testDbContext.UserPokemon.FirstOrDefault(x => x.Nickname == OriginalNickname);
        var newNicknameQueryResult = _testDbContext.UserPokemon.FirstOrDefault(x => x.Nickname == NewNickname);


        // Assert
        result.Should().NotBeNull();
        oldNicknameQueryResult.Should().BeNull();
        newNicknameQueryResult.Should().NotBeNull();
        Assert.Equal(NewNickname, newNicknameQueryResult?.Nickname);
    }


    [Fact]
    public void UpdateTeamById_WithInvalidTeamId_ReturnsNull()
    {
        // Arrange
        Team testTeam = new()
        {
            AppUserId = TestUser1Id,
            TeamName = "test"
        };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();


        CreateUpdateTeamDTO updateTeamDTO = new()
        {
            TeamName = "updatedTest"
        };

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.UpdateTeamById(-1, updateTeamDTO, TestUser1Id);


        // Assert 
        result.Should().BeNull();
    }


    [Fact]
    public void UpdateTeamById_WithInvalidUserId_DoesntUpdateDb()
    {
        // Arrange
        const string OriginalTeamName = "Original";
        const string NewTeamName = "Updated";

        Team testTeam = new()
        {
            AppUserId = TestUser1Id,
            TeamName = OriginalTeamName
        };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();


        CreateUpdateTeamDTO updateTeamDTO = new()
        {
            TeamName = NewTeamName
        };

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.UpdateTeamById(testTeam.Id, updateTeamDTO, TestUser2Id);
        var oldTeamNameQueryResult = _testDbContext.Team.FirstOrDefault(x => x.TeamName == OriginalTeamName);
        var newTeamNameQueryResult = _testDbContext.Team.FirstOrDefault(x => x.TeamName == NewTeamName);

        // Assert 
        result.Should().BeNull();
        newTeamNameQueryResult.Should().BeNull();
        oldTeamNameQueryResult.Should().NotBeNull();
    }




    [Fact]
    public void DeleteTeamById_WithValidId_ReturnsTeam()
    {
        // Arrange
        Team testTeam = new()
        {
            AppUserId = TestUser1Id,
            TeamName = "TestTeam"
        };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();

        int testId = testTeam.Id;

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.DeleteTeamById(testId, TestUser1Id);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(testTeam.TeamName, result?.TeamName);
    }


    [Fact]
    public void DeleteTeamById_WithValidId_RemovesTeamFromDb()
    {
        // Arrange
        Team testTeam = new()
        {
            AppUserId = TestUser1Id,
            TeamName = "TestTeam"
        };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();

        int testId = testTeam.Id;

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.DeleteTeamById(testId, TestUser1Id);
        var dbQueryResult = _testDbContext.Team.FirstOrDefault();

        // Assert
        result.Should().NotBeNull();
        dbQueryResult.Should().BeNull();
    }


    [Fact]
    public void DeleteTeamById_WithTeamIncludingUserPokemon_DeletesUserPokemon()
    {
        // Arrange
        Team testTeam = new()
        {
            AppUserId = TestUser1Id,
            TeamName = "TestTeam"
        };

        UserPokemon testUserPokemon = new()
        {
            TeamId = testTeam.Id,
            Nickname = "TestUserPokemon"
        };

        testTeam.UserPokemon.Add(testUserPokemon);

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();

        int testId = testTeam.Id;

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        
        var result = teamRepository.DeleteTeamById(testId, TestUser1Id);
        var dbQueryResult = _testDbContext.UserPokemon.FirstOrDefault();

        // Assert
        result.Should().NotBeNull();
        dbQueryResult.Should().BeNull();
    }


    [Fact]
    public void DeleteTeamById_WithoutMatchingId_ReturnsNull()
    {
        // Arrange
        Team testTeam = new()
        {
            AppUserId = TestUser1Id,
            TeamName = "TestTeam"
        };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();

        int testId = -1;

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.DeleteTeamById(testId, TestUser1Id);

        // Assert
        result.Should().BeNull();
    }


    [Fact]
    public void DeleteTeamById_WithoutMatchingUserId_LeavesTeamInDb()
    {
        // Arrange
        Team testTeam = new()
        {
            AppUserId = TestUser1Id,
            TeamName = "TestTeam"
        };

        _testDbContext.Team.Add(testTeam);
        _testDbContext.SaveChanges();

        int testId = testTeam.Id;

        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        var result = teamRepository.DeleteTeamById(testId, TestUser2Id);
        var dbQueryResult = _testDbContext.Team.FirstOrDefault();

        // Assert
        result.Should().BeNull();
        dbQueryResult.Should().NotBeNull();
        Assert.Equal(testTeam.TeamName, dbQueryResult?.TeamName);
    }




    [Fact]
    public void CreateTeams_WithValidInput_ReturnsTeams()
    {
        // Arrange
        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        CreateUserPokemonDTO team1PokemonDTO = new()
        {
            Nickname = "team1UserPokemon"
        };

        CreateTeamsDTO team1DTO = new()
        {
            Id = -1,
            TeamName = "TestTeam1",
            UserPokemon = [team1PokemonDTO]
        };

        CreateUserPokemonDTO team2PokemonDTO = new()
        {
            Nickname = "team2UserPokemon"
        };

        CreateTeamsDTO team2DTO = new()
        {
            Id = -2,
            TeamName = "TestTeam2",
            UserPokemon = [team2PokemonDTO]
        };


        List<CreateTeamsDTO> teamDTOs = [
            team1DTO,
            team2DTO,
        ];

        
        var result = teamRepository.CreateTeams(teamDTOs, TestUser1Id);
        var dbQueryResult = _testDbContext.Team.ToList();
        var team1Result = dbQueryResult.FirstOrDefault(x => x.TeamName == team1DTO.TeamName);
        var team2Result = dbQueryResult.FirstOrDefault(x => x.TeamName == team2DTO.TeamName);



        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(dbQueryResult);

        result.Count.Should().Be(teamDTOs.Count);

        team1Result.Should().NotBeNull();
        Assert.Equal(team1DTO.TeamName, team1Result?.TeamName);
        Assert.Equal(team1DTO.UserPokemon.Count, team1Result?.UserPokemon.Count);
        Assert.Equal(team1PokemonDTO.Nickname, team1Result?.UserPokemon[0].Nickname);
        Assert.True(team1Result?.Id >= 1);

        team2Result.Should().NotBeNull();
        Assert.Equal(team2DTO.TeamName, team2Result?.TeamName);
        Assert.Equal(team2DTO.UserPokemon.Count, team2Result?.UserPokemon.Count);
        Assert.Equal(team2PokemonDTO.Nickname, team2Result?.UserPokemon[0].Nickname);
        Assert.True(team2Result?.Id >= 1);
    }


    [Fact]
    public void CreateUpdateTeams_WithEmptyList_ReturnsEmptyList()
    {
        // Arrange
        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        List<CreateTeamsDTO> teamDTOs = [];
        var result = teamRepository.CreateTeams(teamDTOs, TestUser2Id);

        // Assert
        result.Should().BeEquivalentTo(new List<Team>());
    }


    [Fact]
    public void CreateTeams_WithUserPokemon_AddsUserPokemonToDb()
    {
        // Arrange
        var teamRepository = new TeamRepository(_testDbContext);


        // Act
        CreateUserPokemonDTO team1PokemonDTO = new()
        {
            Nickname = "team1UserPokemon"
        };

        CreateTeamsDTO team1DTO = new()
        {
            Id = -1,
            TeamName = "TestTeam1",
            UserPokemon = [team1PokemonDTO]
        };


        List<CreateTeamsDTO> teamDTOs = [team1DTO];

        
        var result = teamRepository.CreateTeams(teamDTOs, TestUser1Id);
        var dbQueryResult = _testDbContext.UserPokemon.ToList();

        // Assert
        result.Should().NotBeNull();
        dbQueryResult.Should().NotBeNull();

        dbQueryResult.Count.Should().Be(team1DTO.UserPokemon.Count);
        Assert.Equal(team1PokemonDTO.Nickname, dbQueryResult[0].Nickname);
    }

}