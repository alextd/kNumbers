﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace Numbers
{
    using UnityEngine;

    [StaticConstructorOnStartup]
    static class StaticConstructorOnGameStart
    {
        public static readonly Texture2D DeleteX = ContentFinder<Texture2D>.Get("UI/Buttons/Delete", true);
        public static readonly Texture2D Info = ContentFinder<Texture2D>.Get("UI/Buttons/InfoButton", true);
        public static readonly Texture2D Predator = ContentFinder<Texture2D>.Get("UI/Icons/Animal/Predator");
        public static readonly Texture2D Tame = ContentFinder<Texture2D>.Get("UI/Icons/Animal/Tame");

        static StaticConstructorOnGameStart()
        {
            //add trainables to animal table
            PawnTableDef animalsTable = NumbersDefOf.Numbers_Animals;

            foreach (PawnColumnDef item in DefDatabase<PawnColumnDef>.AllDefsListForReading.Where(x => x.Worker is PawnColumnWorker_Trainable))
            {
                animalsTable.columns.Insert(animalsTable.columns.FindIndex((PawnColumnDef x) => x.Worker is PawnColumnWorker_Checkbox) - 1, item);
            }

            //add remaining space to my PTDefs
            IEnumerable<PawnTableDef> allPawntableDefs = DefDatabase<PawnTableDef>.AllDefsListForReading.Where(x => x.HasModExtension<DefModExtension_PawnTableDefs>());

            PawnColumnDef remainingspace = DefDatabase<PawnColumnDef>.AllDefsListForReading.First(x => x.Worker is PawnColumnWorker_RemainingSpace);

            IEnumerable<PawnTableDef> ptsDfromPtDses = allPawntableDefs as PawnTableDef[] ?? allPawntableDefs.ToArray();

            foreach (PawnTableDef PTSDfromPTDs in ptsDfromPtDses)
            {
                PTSDfromPTDs.columns.Insert(PTSDfromPTDs.columns.Count, remainingspace);
            }
        }
    }
}