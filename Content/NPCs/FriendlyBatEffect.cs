using FragmentsOfNocturnia.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.NPCs
{
    public class FriendlyBatEffect : ModNPC
    {
        private static readonly int COUNT_DUST_EFFECT = 20; // Number of dust particles to spawn
        public override string Texture => "Terraria/Images/NPC_" + NPCID.IlluminantBat;

        private int lifeTime = 60; // Default timeLeft, can be set on spawn

        public static void SpawnFriendlyBats(Vector2 worldPosition, int count = 3, int despawnTime = 60, bool includeDustEffect = false)
        {
            for (int i = 0; i < count; i++)
            {
                int bat = NPC.NewNPC(
                    source: new EntitySource_TileInteraction(Main.LocalPlayer, (int) worldPosition.X / 16, (int) worldPosition.Y / 16),
                    X: (int)worldPosition.X + 8,
                    Y: (int)worldPosition.Y + 8,
                    Type: ModContent.NPCType<FriendlyBatEffect>()
                );
                Main.npc[bat].velocity = new Vector2(
                    Main.rand.NextFloat(-4f, 4f),
                    Main.rand.NextFloat(-6f, -2f)
                );
                Main.npc[bat].timeLeft = despawnTime;
                Main.npc[bat].netUpdate = true;
            }

            if (includeDustEffect)
            {
                SpawnDustEffect(worldPosition);
            }
        }

        public static void SpawnDustEffect(Vector2 worldPosition)
        {
            for (int i = 0; i < COUNT_DUST_EFFECT; i++)
            {
                Dust.NewDust(
                    Position: worldPosition,
                    Width: 16,
                    Height: 16,
                    Type: ModContent.DustType<NocturneDust>(),
                    SpeedX: Main.rand.NextFloat(-2f, 2f),
                    SpeedY: Main.rand.NextFloat(-2f, 2f)
                );
            }
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.IlluminantBat];
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 12;
            NPC.aiStyle = 14; // Bat AI  
            AIType = NPCID.CaveBat;
            NPC.friendly = true;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.lifeMax = 60;
            NPC.timeLeft = 60; // Default, can be set on spawn  
        }

        public override bool? CanBeHitByItem(Player player, Item item) => false;
        public override bool? CanBeHitByProjectile(Projectile projectile) => false;

        public override bool CanHitNPC(NPC target)
        {
            return false;
        }

        // Allow setting the timer from outside (optional)
        public override void OnSpawn(IEntitySource source)
        {
            // TODO fix passing time left, the value timeLeft can be overwritten
            //if (NPC.timeLeft > 0)
            //{
            //    lifeTime = NPC.timeLeft;
            //}
        }

        public override void AI()
        {
            // Decrement timeLeft manually to prevent vanilla reset  
            lifeTime--;
            if (lifeTime <= 0)
            {
                NPC.active = false;
            }
        }
    }
}