using System.Collections.Generic;
using System.Threading;
using System.IO;
using System;
using TrueGearSDK;
using System.Linq;
using MelonLoader;



namespace MyTrueGear
{
    public class TrueGearMod
    {
        private static TrueGearPlayer _player = null;

        private static ManualResetEvent pauseMRE = new ManualResetEvent(true);
        private static ManualResetEvent heartbeatMRE = new ManualResetEvent(false);
        private static ManualResetEvent laserCuttingMRE = new ManualResetEvent(false);
        private static ManualResetEvent lowFoodMRE = new ManualResetEvent(false);
        private static ManualResetEvent lowOxygenMRE = new ManualResetEvent(false);
        private static ManualResetEvent lowWaterMRE = new ManualResetEvent(false);
        private static ManualResetEvent precursorTeleporterMRE = new ManualResetEvent(false);
        private static ManualResetEvent weldMRE = new ManualResetEvent(false);
        private static ManualResetEvent extinguisherMRE = new ManualResetEvent(false);
        private static ManualResetEvent stasisRifleChargingMRE = new ManualResetEvent(false);
        private static ManualResetEvent airBladderMRE = new ManualResetEvent(false);
        private static ManualResetEvent scannerToolMRE = new ManualResetEvent(false);
        private static ManualResetEvent builderToolMRE = new ManualResetEvent(false);
        private static ManualResetEvent leftHandDrillMRE = new ManualResetEvent(false);
        private static ManualResetEvent rightHandDrillMRE = new ManualResetEvent(false);

        public TrueGearMod()
        {
            _player = new TrueGearPlayer("264710","Subnautica");
            _player.Start();
            new Thread(new ThreadStart(this.HeartBeat)).Start();
            new Thread(new ThreadStart(this.LaserCutting)).Start();
            new Thread(new ThreadStart(this.LowFood)).Start();
            new Thread(new ThreadStart(this.LowOxygen)).Start();
            new Thread(new ThreadStart(this.LowWater)).Start();
            new Thread(new ThreadStart(this.PrecursorTeleporter)).Start();
            new Thread(new ThreadStart(this.Weld)).Start();
            new Thread(new ThreadStart(this.Extinguisher)).Start();
            new Thread(new ThreadStart(this.StasisRifleCharging)).Start();
            new Thread(new ThreadStart(this.AirBladder)).Start();
            new Thread(new ThreadStart(this.ScannerTool)).Start();
            new Thread(new ThreadStart(this.BuilderTool)).Start();
            new Thread(new ThreadStart(this.LeftHandDrill)).Start();
            new Thread(new ThreadStart(this.RightHandDrill)).Start();
        }

        public void HeartBeat()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                heartbeatMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("HeartBeat");
                _player.SendPlay("HeartBeat");
                Thread.Sleep(1000);
            }            
        }

        public void LaserCutting()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                laserCuttingMRE.WaitOne(); 
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("LaserCutting");
                _player.SendPlay("LaserCutting");
                Thread.Sleep(150);
            }
        }

        public void LowFood()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                lowFoodMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("LowFood");
                _player.SendPlay("LowFood");
                Thread.Sleep(1000);
            }
        }

        public void LowOxygen()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                lowOxygenMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("LowOxygen");
                _player.SendPlay("LowOxygen");
                Thread.Sleep(1000);
            }
        }

        public void LowWater()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                lowWaterMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("LowWater");
                _player.SendPlay("LowWater");
                Thread.Sleep(1000);
            }
        }

        public void PrecursorTeleporter()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                precursorTeleporterMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("PrecursorTeleporter");
                _player.SendPlay("PrecursorTeleporter");
                Thread.Sleep(150);
            }
        }

        public void Weld()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                weldMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("Weld");
                _player.SendPlay("Weld");
                Thread.Sleep(150);
            }
        }

        public void Extinguisher()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                extinguisherMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("Extinguisher");
                _player.SendPlay("Extinguisher");
                Thread.Sleep(150);
            }
        }

        public void StasisRifleCharging()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                stasisRifleChargingMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("StasisRifleCharging");
                _player.SendPlay("StasisRifleCharging");
                Thread.Sleep(150);
            }
        }

        public void AirBladder()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                airBladderMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("AirBladder");
                _player.SendPlay("AirBladder");
                Thread.Sleep(150);
            }
        }

        public void ScannerTool()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                scannerToolMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("ScannerTool");
                _player.SendPlay("ScannerTool");
                Thread.Sleep(200);
            }
        }

        public void BuilderTool()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                builderToolMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("BuilderTool");
                _player.SendPlay("BuilderTool");
                Thread.Sleep(200);
            }
        }

        public void LeftHandDrill()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                leftHandDrillMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("LeftHandDrill");
                _player.SendPlay("LeftHandDrill");
                Thread.Sleep(150);
            }
        }

        public void RightHandDrill()
        {
            while (true)
            {
                pauseMRE.WaitOne();
                rightHandDrillMRE.WaitOne();
                MelonLogger.Msg("-----------------------------------------------");
                MelonLogger.Msg("RightHandDrill");
                _player.SendPlay("RightHandDrill");
                Thread.Sleep(150);
            }
        }


        public void Play(string Event)
        { 
            _player.SendPlay(Event);
        }


        public void Pause()
        {
            pauseMRE.Reset();
        }

        public void NoPause()
        {
            pauseMRE.Set();
        }



        public void StartHeartBeat()
        {
            heartbeatMRE.Set();
        }

        public void StopHeartBeat()
        {
            heartbeatMRE.Reset();
        }


        public void StartLaserCutting()
        {
            laserCuttingMRE.Set();
        }

        public void StopLaserCutting()
        {
            laserCuttingMRE.Reset();
        }

        public void StartLowFood()
        {
            lowFoodMRE.Set();
        }

        public void StopLowFood()
        {
            lowFoodMRE.Reset();
        }

        public void StartLowOxygen()
        {
            lowOxygenMRE.Set();
        }

        public void StopLowOxygen()
        {
            lowOxygenMRE.Reset();
        }

        public void StartLowWater()
        {
            lowWaterMRE.Set();
        }

        public void StopLowWater()
        {
            lowWaterMRE.Reset();
        }

        public void StartPrecursorTeleporter()
        {
            precursorTeleporterMRE.Set();
        }

        public void StopPrecursorTeleporter()
        {
            precursorTeleporterMRE.Reset();
        }

        public void StartWeld()
        {
            weldMRE.Set();
        }

        public void StopWeld()
        {
            weldMRE.Reset();
        }

        public void StartExtinguisher()
        {
            extinguisherMRE.Set();
        }

        public void StopExtinguisher()
        {
            extinguisherMRE.Reset();
        }

        public void StartStasisRifleCharging()
        {
            stasisRifleChargingMRE.Set();
        }

        public void StopStasisRifleCharging()
        {
            stasisRifleChargingMRE.Reset();
        }

        public void StartAirBladder()
        {
            airBladderMRE.Set();
        }

        public void StopAirBladder()
        {
            airBladderMRE.Reset();
        }

        public void StartScannerTool()
        {
            scannerToolMRE.Set();
        }

        public void StopScannerTool()
        {
            scannerToolMRE.Reset();
        }

        public void StartBuilderTool()
        {
            builderToolMRE.Set();
        }

        public void StopBuilderTool()
        {
            builderToolMRE.Reset();
        }

        public void StartLeftHandDrill()
        {
            leftHandDrillMRE.Set();
        }

        public void StopLeftHandDrill()
        {
            leftHandDrillMRE.Reset();
        }

        public void StartRightHandDrill()
        {
            rightHandDrillMRE.Set();
        }

        public void StopRightHandDrill()
        {
            rightHandDrillMRE.Reset();
        }

    }
}
