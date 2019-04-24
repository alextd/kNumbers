﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Harmony;

namespace Numbers
{
    public class PawnColumnWorker_AnimalEggProgress : PawnColumnWorker_Text
    {
        protected override string GetTextFor(Pawn pawn)
        {
            if (pawn.ageTracker.CurLifeStage.reproductive)
            {
                var comp = pawn.AllComps.FirstOrDefault(x => x is CompEggLayer);

                if (comp != null)
                    return ((float)Traverse.Create((CompEggLayer)comp).Field("eggProgress").GetValue()).ToStringPercent();
            }
            return null;
        }

        public override int Compare(Pawn a, Pawn b)
            => GetScoreFor(a).CompareTo(GetScoreFor(b));

        private float GetScoreFor(Pawn pawn)
        {
            if (pawn.ageTracker.CurLifeStage.reproductive)
            {
                var comp = pawn.AllComps.FirstOrDefault(x => x is CompEggLayer);

                if (comp != null)
                    return (float)Traverse.Create((CompEggLayer)comp).Field("eggProgress").GetValue();
            }
            return -1;
        }
    }
}
