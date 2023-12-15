using Xylia.Preview.Data.Models;
using Xylia.Preview.Document;

namespace Xylia.Preview.Data.Client;
internal partial class SqlParser
{
    /// <summary>
    /// {expr0}, {expr1}, ..., {exprN}
    /// </summary>
    private IEnumerable<BsonExpression> ParseListOfExpressions()
    {
        while(true)
        {
            var expr = BsonExpression.Create(_tokenizer, BsonExpressionParserMode.Full, _parameters);

            yield return expr;

            var next = _tokenizer.LookAhead();
            
            if (next.Type == TokenType.Comma)
            {
                _tokenizer.ReadToken();
            }
            else
            {
                yield break;
            }
        }
    }

    /// <summary>
    /// {doc0}, {doc1}, ..., {docN} {EOF|;}
    /// </summary>
    private IEnumerable<AttributeDocument> ParseListOfDocuments()
    {
        //var reader = new JsonReader(_tokenizer);

        //while (true)
        //{
        //    var value = reader.Deserialize();

        //    if (value.IsAttributeDocument)
        //    {
        //        yield return value as AttributeDocument;
        //    }
        //    else
        //    {
        //        throw BnsException.UnexpectedToken("Value must be a valid document", _tokenizer.Current);
        //    }

        //    var next = _tokenizer.LookAhead();

        //    if (next.Type == TokenType.Comma)
        //    {
        //        _tokenizer.ReadToken();
        //    }
        //    else
        //    {
        //        next.Expect(TokenType.EOF, TokenType.SemiColon);

        //        _tokenizer.ReadToken();

        //        yield break;
        //    }
        //}

        return null;
    }
}