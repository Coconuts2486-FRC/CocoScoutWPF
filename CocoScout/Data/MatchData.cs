using Newtonsoft.Json;
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

    [JsonObject]
    public class MatchData
    {
        /// <summary>
        /// Team number.
        /// </summary>
        [JsonProperty(PropertyName = "Team Number")]
        public uint TeamNumber  { get; set; }
        /// <summary>
        /// Match number.
        /// </summary>
        [JsonProperty(PropertyName = "Match Number")]
        public byte MatchNumber { get; set; }

        [JsonProperty(PropertyName = "User")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "Event")]
        public string Event { get; set; }

        #region TeleOp
        #region Gears
        /// <summary>
        /// How many gears did they place?
        /// </summary>
        [JsonProperty(PropertyName = "TeleOp Gears Placed")]
        public byte TeleOpGearsPlaced { get; set; }
        /// <summary>
        /// Can they pick up gears from the ground?
        /// </summary>
        [JsonProperty(PropertyName = "TeleOp Gear Pickup Ground")]
        public bool TeleOpGearsPickupGround { get; set; }
        #endregion

        #region Fuel
        /// <summary>
        /// How much pressure did they build?
        /// </summary>
        [JsonProperty(PropertyName = "TeleOp Pressure")]
        public byte TeleOpPressure { get; set; }
        /// <summary>
        /// How fast could they shoot fuel?
        /// </summary>
        [JsonProperty(PropertyName = "TeleOp Fuel Speed")]
        public Speed TeleOpFuelSpeed { get; set; }
        #endregion

        #region Climbing
        [JsonProperty(PropertyName = "Climbed")]
        public bool Climbed
        {
            get
            {
                if (ClimbSpeed == Speed.None)
                    return false;
                else return true;
            }
        }
        [JsonProperty(PropertyName = "Climb Speed")]
        public Speed ClimbSpeed { get; set; } = Speed.None;
        #endregion
        #endregion
        #region Auto
        #region Gears
        /// <summary>
        /// Whether or not they placed a gear.
        /// </summary>
        [JsonProperty(PropertyName = "Auto Placed Gear")]
        public bool AutoPlacedGear
        {
            get
            {
                if (AutoGearSpot != GearPlacement.None)
                    return true;
                else return false;
            }
        }
        /// <summary>
        /// Where they placed a gear.
        /// </summary>
        [JsonProperty(PropertyName = "Auto Gear Spot")]
        public GearPlacement AutoGearSpot { get; set; } = GearPlacement.None;
        #endregion

        #region Fuel
        /// <summary>
        /// How much pressure did they build?
        /// </summary>
        [JsonProperty(PropertyName = "Auto Pressure")]
        public byte AutoPressure { get; set; }
        /// <summary>
        /// How fast could they shoot fuel?
        /// </summary>
        [JsonProperty(PropertyName = "Auto Fuel Speed")]
        public Speed AutoFuelHighSpeed { get; set; } = Speed.None;
        #endregion
        #endregion

        public void ResetAllFields()
        {
            TeamNumber = 0;
            MatchNumber = 0;
            TeleOpGearsPlaced = 0;
            TeleOpGearsPickupGround = false;
            TeleOpPressure = 0;
            TeleOpFuelSpeed = Speed.None;
            ClimbSpeed = Speed.None;
            AutoGearSpot = GearPlacement.None;
            AutoPressure = 0;
            AutoFuelHighSpeed = Speed.None;
        }

        public override string ToString()
        {
            return
                "Team Number:    " + TeamNumber +
                "\nMatch Number:   " + MatchNumber +
                "\nUser:           " + User +
                "\nEvent:          " + Event +
                "\nA Placed Gear:  " + AutoPlacedGear +
                "\nA Gear Spot:    " + AutoGearSpot +
                "\nA Pressure:     " + AutoPressure +
                "\nA Fuel Speed:   " + AutoFuelHighSpeed +
                "\nT Gears Placed: " + TeleOpGearsPlaced +
                "\nGear Pickup:    " + TeleOpGearsPickupGround +
                "\nT Pressure:     " + TeleOpPressure +
                "\nT Fuel Speed:   " + TeleOpFuelSpeed +
                "\nClimbed:        " + Climbed +
                "\nClimb Speed:    " + ClimbSpeed;
        }
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