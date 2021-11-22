using MyLeague.Football.Data.Generators;
using System.ComponentModel.DataAnnotations;

namespace MyLeague.Football.Data.Models
{
    public class PlayerAttributes
    {
        public PlayerAttributes() { }

        [Key]
        public int Id { get; set; }
        public int Overall { get; set; }
        public int Speed { get; set; }
        public int Acceleration { get; set; }
        public int Strength { get; set; }
        public int Awareness { get; set; }
        public int Agility { get; set; }
        public int Stamina { get; set; }
        public int Injury { get; set; }
        public int Toughness { get; set; }
        public int Jumping { get; set; }
        public int HitPower { get; set; }
        public int Tackle { get; set; }
        public int Pursuit { get; set; }
        public int PowerMove { get; set; }
        public int FinesseMove { get; set; }
        public int BlockShedding { get; set; }
        public int PlayRecognition { get; set; }
        public int ZoneCoverage { get; set; }
        public int ManCoverage { get; set; }
        public int Catching { get; set; }
        public int CatchInTraffic { get; set; }
        public int RunBlock { get; set; }
        public int PassBlock { get; set; }
        public int LeadBlock { get; set; }
        public int ImpactBlock { get; set; }
        public int RouteRunning { get; set; }
        public int Carrying { get; set; }
        public int BreakTackle { get; set; }
        public int Trucking { get; set; }
        public int Elusiveness { get; set; }
        public int Release { get; set; }
        public int Press { get; set; }
        public int ThrowPower { get; set; }
        public int DeepBall { get; set; }
        public int MediumAccuracy { get; set; }
        public int ShortAccuracy { get; set; }
        public int ThrowOnRun { get; set; }
        public int PlayAction { get; set; }
        public int ThrowUnderPressure { get; set; }
        public int BreakSack { get; set; }
        public int KickReturn { get; set; }
        public int KickPower { get; set; }
        public int KickAccuracy { get; set; }
    }
}
