using System.IO;
using FragmentsOfNocturnia.Content.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class FragmentsOfNocturnia : Mod
	{
        // Public static reference to your mod instance
        public static FragmentsOfNocturnia Instance { get; private set; }

        public FragmentsOfNocturnia()
        {
            Instance = this;
        }

        public override void Load()
        {
            Logger.InfoFormat("FragmentsOfNocturnia: Mod loaded successfully!");
        }

        internal enum MessageType : byte
        {
            SpawnBats
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            //
            // TODO: when more packet types are added
            //
            // since HandlePacket is an override on Mod, this method has to be here.
            // However, this should call a specific method for each message rather than have 
            // them all here. These could be moved to separate handler classes.
            //



            MessageType msgType = (MessageType) reader.ReadByte();
            Logger.Info("Packet recieved: " + msgType);
            if (msgType == MessageType.SpawnBats)
            {
                // Since the caller writes data, that code is tightly coupled to this.
                // There should be a helper that encpasulates both reads and writes
                // to prevent errors and make it easier to modify.
                int left = reader.ReadInt32();
                int top = reader.ReadInt32();
                int count = reader.ReadInt32();
                int time = reader.ReadInt32();

                if (Main.netMode == NetmodeID.Server)
                {
                    FriendlyBatEffect.SpawnFriendlyBats(
                        new Vector2((left + 1) * 16, top * 16), // center of chest
                        count, 
                        time, 
                        true);
                }
            }
        }
    }
}
