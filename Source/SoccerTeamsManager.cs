using System;
using System.Collections.Generic;
using Codenation.Challenge.Exceptions;
using System.Linq;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        public SoccerTeamsManager()
        {
            this.Teams = new List<Team>();
        }
        public List<Team> Teams { get; set; }
        
        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            var IdTeams = GetTeams();
            if (IdTeams.Contains(id))
            {
                throw new UniqueIdentifierException();
            }
            else
            {
                Team team = new Team(id,name,createDate,mainShirtColor,secondaryShirtColor);
                Teams.Add(team);
            }
        }
        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            var IdTeams = GetTeams();
            if (IdTeams.Contains(teamId))
            {   

                Player player = new Player(id,name,birthDate,skillLevel,salary);
                Teams.Single<Team>(x => x.Id == teamId).PlayerList.Add(player);
            }
            else
            {
                throw new TeamNotFoundException();
            }
        }
        public void SetCaptain(long playerId)
        {
            foreach (Team time in Teams)
            {
                var playersId = GetTeamPlayers(time.Id);

                if (playersId.Contains(playerId))
                {
                    time.CapitainId = playerId;
                    return;
                }
            }

            throw new PlayerNotFoundException();
        }

        public long GetTeamCaptain(long teamId)
        {
            var IdTeam = GetTeams();
            if (IdTeam.Contains(teamId))
            {
                var CaptainId = Teams.Single(x => x.Id == teamId).CapitainId;
                if (CaptainId != null)
                    return (long)CaptainId;
                else
                    throw new CaptainNotFoundException();
            }
            else
            {
                throw new TeamNotFoundException();
            }

        }
        public string GetPlayerName(long playerId)
        {
            List<Player> ListPlayer = GetAllPlayers();
            var list = ListPlayer.Select<Player, long>(x => x.Id).ToList();
            if (list.Contains(playerId))
            {
                return (string) ListPlayer.Single<Player>(x => x.Id == playerId).Name;
            }
            else
            {
                throw new PlayerNotFoundException();
            }
            
        }
        public string GetTeamName(long teamId)
        {

            var IdTeams = GetTeams();
            if (IdTeams.Contains(teamId))
            {
                Team time = Teams.Single<Team>(x => x.Id == teamId);
                return (string)time.Name;
            }
            else
            {
                throw new TeamNotFoundException();
            }
        }
        public List<long> GetTeamPlayers(long teamId)
        {
            List<long> IdPlayers = new List<long>();
            var TimeId = GetTeams();
            if (TimeId.Contains(teamId))
            {
                Team time = Teams.Single<Team>(x => x.Id == teamId);
                foreach (Player item in time.PlayerList)
                {
                    IdPlayers.Add(item.Id);
                }
            }
            else 
            {
                throw new TeamNotFoundException();
            }

            return IdPlayers.OrderBy(x=>x).ToList<long>();
        }
        public long GetBestTeamPlayer(long teamId)
        {
            var IdTeams = GetTeams();
            if (IdTeams.Contains(teamId))
            {
                Team time = Teams.Single<Team>(x => x.Id == teamId);
                int HigherSkill = (int)time.PlayerList.Max<Player>(x => x.SkillLevel);
                return time.PlayerList.OrderBy(x => x.Id).First<Player>(x => x.SkillLevel == HigherSkill).Id;
            }
            else
            {
                throw new TeamNotFoundException();
            }
        }
        public long GetOlderTeamPlayer(long teamId)
        {
            var TeamsId = GetTeams();
            if (TeamsId.Contains(teamId))
            {
                return Teams.Single(x => x.Id == teamId).PlayerList.OrderBy(x => x.BirthDate).First().Id;
            }
            throw new TeamNotFoundException();
        }
        public List<long> GetTeams()
        {
            List<long> IdTeams = new List<long>();

            foreach (Team item in Teams)
            {
                IdTeams.Add(item.Id);
            }
            return IdTeams.OrderBy(x=>x).ToList<long>();
        }
        public long GetHigherSalaryPlayer(long teamId)
        {
            var IdTeams = GetTeams();
            if (IdTeams.Contains(teamId))
            {
                Team time = Teams.Single<Team>(x => x.Id == teamId);
                long HigherSalary = (long)time.PlayerList.Max<Player>(x => x.Salary);
                return time.PlayerList.OrderBy(x => x.Id).First<Player>(x => x.Salary == HigherSalary).Id;
            }
            else
            {
                throw new TeamNotFoundException();
            }
        }
        public decimal GetPlayerSalary(long playerId)
        {
            List<Player> ListPlayer = GetAllPlayers();
            var list = ListPlayer.Select<Player, long>(x => x.Id).ToList();
            if (list.Contains(playerId))
            {
                return ListPlayer.Single<Player>(x => x.Id == playerId).Salary;
            }
            throw new PlayerNotFoundException();
        }
        public List<long> GetTopPlayers(int top)
        {   
            List<Player> list = GetAllPlayers();
            return list.OrderByDescending(x => x.SkillLevel).ThenBy(x => x.Id).Select<Player, long>(x => x.Id).Take(top).ToList();
        }
        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            var IdTeams = GetTeams();

            if(IdTeams.Contains(teamId) && IdTeams.Contains(visitorTeamId))
            {
                Team HomeTeam = Teams.Single<Team>(x => x.Id == teamId);
                Team VisitorTeam = Teams.Single<Team>(x => x.Id == visitorTeamId);

                if (HomeTeam.MainShirtColor==VisitorTeam.MainShirtColor)
                {
                    return (string)VisitorTeam.SecondaryShirtColor;
                }
                else
                {
                    return (string)VisitorTeam.MainShirtColor;
                }
            }
            else
            {
                throw new TeamNotFoundException();
            }
        }
        public List<Player> GetAllPlayers()
        {
            List<Player> PlayerList = new List<Player>();
            foreach (Team time in Teams)
            {
                PlayerList.AddRange(time.PlayerList);
            }
            return PlayerList;
        }
    }
    public class Team
    {
        public Team(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            this.Id = id;
            this.Name = name;
            this.CreateDate = createDate;
            this.MainShirtColor = mainShirtColor;
            this.SecondaryShirtColor = secondaryShirtColor;
            this.PlayerList = new List<Player>();
            this.CapitainId = null;
        }
        public long? CapitainId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string MainShirtColor { get; set; }
        public string SecondaryShirtColor { get; set; }
        public List<Player> PlayerList { get; set; }
    }
    public class Player
    {
        public Player(long id,string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            this.Id = id;
            this.Name = name;
            this.BirthDate = birthDate;
            this.SkillLevel = skillLevel;
            this.Salary = salary;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int SkillLevel { get; set; }
        public decimal Salary { get; set; }
}
}
