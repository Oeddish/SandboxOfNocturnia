using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using FragmentsOfNocturnia.Content.System;

public class BatSparkleGlobalNPC : GlobalNPC
{
    public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
    {
        // Apply only to bat NPCs
        return NpcType.IsBat(entity.type);
    }

    public override void AI(NPC npc)
    {
        // Spawn sparkle dust around the bat
        if (Main.rand.NextBool(3)) // 1 in 3 chance per tick
        {
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.GemAmethyst, 0f, 0f, 150, Color.White, 1.2f);
        }
    }

    public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        // Draw the normal sprite
        return true; // Let vanilla draw happen
    }

    public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        // Draw a colored overlay (glow effect)
        Texture2D texture = Terraria.GameContent.TextureAssets.Npc[npc.type].Value;
        Vector2 position = npc.Center - screenPos;
        Vector2 origin = new Vector2(texture.Width / 2, texture.Height / Main.npcFrameCount[npc.type] / 2);
        Rectangle frame = npc.frame;

        // Draw a semi-transparent purple glow
        Color glowColor = new Color(180, 0, 255, 120); // RGBA
        spriteBatch.Draw(
            texture,
            position,
            frame,
            glowColor,
            npc.rotation,
            origin,
            npc.scale,
            SpriteEffects.None,
            0f
        );
    }
}
