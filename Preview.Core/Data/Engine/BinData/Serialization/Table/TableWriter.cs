using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Engine.BinData.Serialization;
internal class TableWriter
{
    public static int GlobalCompressionBlockSize = ushort.MaxValue;
    private readonly RecordCompressedWriter _recordCompressedWriter;
    private readonly RecordUncompressedWriter _recordUncompressedWriter;

    public TableWriter(int compressionBlockSize = -1)
    {
        if (compressionBlockSize == -1)
            compressionBlockSize = GlobalCompressionBlockSize;

        _recordCompressedWriter = new RecordCompressedWriter(compressionBlockSize);
        _recordUncompressedWriter = new RecordUncompressedWriter();
    }


    public void WriteTo(DataArchiveWriter writer, Table table, bool is64Bit)
    {
        // LazyTable
        if (table.Archive != null)
        {
            using var stream = table.Archive.LazyStream();
            writer.Write(table.ElementCount);
            writer.Write(table.Type);
            writer.Write(table.MajorVersion);
            writer.Write(table.MinorVersion);
            writer.Write(table.Size);
            writer.Write(table.IsCompressed);

            stream.Seek(12, SeekOrigin.Begin);
            stream.CopyTo(writer);
            return;
        }

        writer.Write(table.ElementCount);
        writer.Write(table.Type);
        writer.Write(table.MajorVersion);
        writer.Write(table.MinorVersion);

        if (table.IsCompressed) WriteCompressed(writer, table);
        else WriteLoose(writer, table, is64Bit);
    }

    private void WriteCompressed(DataArchiveWriter writer, Table table)
    {
        var recordCompressedWriter = _recordCompressedWriter;
        recordCompressedWriter.BeginWrite(writer);

        foreach (var record in table.Records.OrderBy(x => x.RecordVariationId).ThenBy(x => x.RecordId))
        {
            recordCompressedWriter.WriteRecord(writer, record.Data, record.StringLookup.Data);
        }

        recordCompressedWriter.EndWrite(writer);
    }

    private void WriteLoose(DataArchiveWriter writer, Table table, bool is64Bit)
    {
        // HACK: read config or find reason
        if (table.RecordCountOffset == 0 &&
            table.Name != null && CountOffsetTable.Contains(table.Name))
            table.RecordCountOffset = 1;

        _recordUncompressedWriter.SetRecordCountOffset(table.RecordCountOffset);
        _recordUncompressedWriter.BeginWrite(writer, is64Bit && !table.IsCompressed && table.ElementCount == 1);

        foreach (var record in table.Records)
        {
            _recordUncompressedWriter.WriteRecord(writer, record.Data);
        }

        _recordUncompressedWriter.EndWrite(writer, table.Padding,
            table.Records.Count == 0
                ? new MemoryStream()
                : new MemoryStream(table.Records.First().StringLookup.Data));
    }


    public static string[] CountOffsetTable { get; set; } = [
        "account-post-charge",
        "arenamatchingrule",
        "arenaportal",
        "attraction-group",
        "attractionrewardsummary",
        "auto-combat-customized-skill-cast-condition",
        "auto-combat-skill-cast-condition",
        "badge-appearance",
        "battleroyalfieldeffectpouchmesh",
        "benefit-ability",
        "board-gacha-reward",
        "board-gacha",
        "boast",
        "bossnpc",
        "campfire",
        "cave2",
        "challenge-party",
        "challengelistreward",
        "cinematic",
        "contents-guide",
        "contents-reset",
        "contentsjournal",
        "contentsjournal2noti",
        "contentsjournalrecommenditem",
        "contributionreward",
        "craft-group-recipe",
        "craft-recipe-step",
        "difficulty-type-modify",
        "district",
        "duel-bot-challenge-strategic-tool",
        "duel-bot-training-room-version",
        "duel-bot",
        "duel-observer-skill-slot",
        "duel",
        "effect-group",
        "emoticon",
        "envresponse",
        "event-contents",
        "field-item-move-anim",
        "fielditemdrop",
        "formula-constant",
        "game-message",
        "game-tip",
        "guild-battle-field-zone",
        "guild-discount",
        "guildcustomizepreset",
        "indicator-idle",
        "item-buy-price",
        "item-event",
        "item-graph-seed-group",
        "item-graph",
        "item-random-ability-section",
        "item-transform-retry-cost",
        "item-usable-group",
        "itemexchange",
        "itemskill",
        "itemtransformupgradeitem",
        "itemusablerelation",
        "jumpingcharacter2",
        "linkmoveanim",
        "loadingimage",
        "map-group-2",
        "market-register-amount-tax-rate",
        "partymatch",
        "passive-effect-move-anim",
        "pcskill3",
        "pet-food-recovery",
        "pet",
        "randombox-preview",
        "set-item",
        "skill-by-equipment",
        "skill-combo-2",
        "skill-modify-info-group",
        "skill-modify-info",
        "skilldashattribute3",
        "skillgatherrange3",
        "skillmodifylimit",
        "skillresultcontroll3",
        "skillsystematizationgroup",
        "skilltargetfilter3",
        "skilltooltipattribute",
        "slatescroll",
        "slatescrollstone",
        "soul-npc-skill",
        "standidle",
        "static-chat-channel",
        "summonedbeautyshop",
        "summonedstandidle",
        "talksocial",
        "teen-body-material",
        "unlocated-store",
        "user-command",
        "virtual-item",
        "weeklytimetable",
        "zoneenv2place",
        "zoneteleportposition",
        "zonetriggereventcond",
        "zonetriggereventstage",
    ];
}