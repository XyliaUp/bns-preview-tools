namespace Xylia.Preview.Data.Client;
internal partial class SqlParser
{
    /// <summary>
    /// INSERT INTO {collection} VALUES {doc0} [, {docN}] [ WITH ID={type} ] ]
    /// </summary>
    private DataReader ParseInsert()
    {
        _tokenizer.ReadToken().Expect("INSERT");
        _tokenizer.ReadToken().Expect("INTO");

        var collection = _tokenizer.ReadToken().Expect(TokenType.Word).Value;

        //var autoId = this.ParseWithAutoId();

        //_tokenizer.ReadToken().Expect("VALUES");

        //// get list of documents (return an IEnumerable)
        //// will validate EOF or ;
        //var docs = this.ParseListOfDocuments();

        //var result = _engine.Insert(collection, docs, autoId);

        //var test = new DataReader(result);
        return null;
    }
}