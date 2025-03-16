using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content;
//ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur, settings, this.owner);
public class KokoModPlayer : ModPlayer
{
	public bool slimePet;

	//EVERYTHING BELOW THIS IS NOT RELATED TO THE SLIME

	public Projectile tracer = null; //Tracer round used in Subtitles's gun alt fire
	public NPC tracerTarget = null;

	public void setTracer(Projectile proj){
		//I only want 1 tracer allowed per player so the tracking bullets dont get confused
		if(tracer != null){
			tracer.Kill();
		}
		tracer = proj;
	}

	public Projectile getTracer(){
		return this.tracer;
	}

	public void resetTracer(){
		this.tracer = null;
	}

	public NPC getTracerTarget(){
		if(tracer == null){
			return null;
		}
		return tracerTarget;
	}

}