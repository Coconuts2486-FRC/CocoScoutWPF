using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoScout.Data
{
    /// <summary>
    /// Enumerable for speed settings.
    /// Speeds available are none, slow, medium, and fast.
    /// Used in <see cref="AutoData.FuelHighSpeed"/>.
    /// </summary>
    public enum Speed
    {
        None,
        Slow,
        Medium,
        Fast
    }

    /// <summary>
    /// Enumerable for the boiler tiers.
    /// Settings available are none (did not shoot), low, and high.
    /// </summary>
    public enum ShootTier
    {
        None,
        Low,
        High
    }

    /// <summary>
    /// Enumerable for the different placements of the gear.
    /// </summary>
    public enum GearPlacement
    {
        None,
        BoilerSide,
        Center,
        HopperSide
    }

    public class MatchData
    {
        public MatchData() : this(null, null) { }
        public MatchData(TeleOpData tData, AutoData aData)
        {
            teleOpData = tData;
            autoData   = aData;
        }

        /// <summary>
        /// Team number.
        /// </summary>
        public uint TeamNumber  { get; set; }
        /// <summary>
        /// Match number.
        /// </summary>
        public byte MatchNumber { get; set; }

        public string User
        {
            get
            {
                return StaticData.Settings.UserName;
            }
        }

        public string Event
        {
            get
            {
                return StaticData.Settings.Event;
            }
        }

        public TeleOpData teleOpData;
        public AutoData autoData;
    }

    public class TeleOpData
    {
        #region Gears
        /// <summary>
        /// How many gears did they place?
        /// </summary>
        public byte GearsPlaced       { get; set; }
        /// <summary>
        /// Can they pick up gears from the ground?
        /// </summary>
        public bool GearsPickupGround { get; set; }
        #endregion

        #region Fuel
        /// <summary>
        /// How much pressure did they build?
        /// </summary>
        public byte Pressure  { get; set; }
        /// <summary>
        /// How fast could they shoot fuel?
        /// </summary>
        public Speed FuelSpeed { get; set; }
        #endregion

        #region Climbing
        public bool Climbed
        {
            get
            {
                if (ClimbSpeed == Speed.None)
                    return false;
                else return true;
            }
        }
        public Speed ClimbSpeed { get; set; } = Speed.None;
        #endregion
    }

    public class AutoData
    {
        #region Gears
        /// <summary>
        /// Whether or not they placed a gear.
        /// </summary>
        public bool PlacedGear
        {
            get
            {
                if (GearSpot != GearPlacement.None)
                    return true;
                else return false;
            }
        }
        /// <summary>
        /// Where they placed a gear.
        /// </summary>
        public GearPlacement GearSpot { get; set; } = GearPlacement.None;
        #endregion

        #region Fuel
        /// <summary>
        /// How much pressure did they build?
        /// </summary>
        public byte  Pressure { get; set; }
        /// <summary>
        /// How fast could they shoot fuel?
        /// </summary>
        public Speed FuelHighSpeed { get; set; } = Speed.None;
        #endregion
    }
}