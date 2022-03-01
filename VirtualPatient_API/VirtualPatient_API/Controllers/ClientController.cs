using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DataAccess;
namespace VirtualPatient_API.Controllers
{
    public class ClientController : ApiController
    {
        // GET api/values/5
        public HttpResponseMessage Get([FromUri] string Question)
        {
            string connectionString = "Data Source=DESKTOP-PBFPR1A;Initial Catalog=VirtualPatient_DB;Integrated Security=True";

            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();

            //Try to match the user's question to an alias question to get a target question
            string sql = "SELECT TOP 1 aliasTokenID, questionText " +
                    " From AliasQuestions " +
                    " where questionText LIKE '" + Question + "' ";

            SqlCommand command = new SqlCommand(sql, cnn);
            //BUGNOTE: Check the email for SQL injection attack
            //command.Parameters.AddWithValue("@quest", Question);
            SqlDataReader SqlDataReader;
            //command.Parameters.AddWithValue("@quest", Question);
            SqlDataReader = command.ExecuteReader();
            string matchedAliasQuestionText = "";
            int matchedAliasQuestionID = 0;
            while (SqlDataReader.Read())
            {
                matchedAliasQuestionText = (string)SqlDataReader[1];
                matchedAliasQuestionID = (int)SqlDataReader[0];
            }
            SqlDataReader.Close();
            command.Dispose();

            //Get VP Answer corresponding to Target question or
            //  indicate to user that VP could not understand and ask again
            //BUGNOTE: Could do a question of keyword - ie DId you mean XXX?
            //      and add it to alias questions

            string matchedVPAnswerText = "";
            if (matchedAliasQuestionID != 0)
            {
                //ALias question found
                //  get VP Answer to Target Question corresponding to VP COndition and Severity
                sql = "Select Z.AnswerText FROM  TargetQuestionAlias X, QuestionAssignment Y, Answer Z, TargetQuestion W where " +
                    " Y.answerId = Z.answerId " +
                    " AND Y.questionId = W.targetQuestionId " +
                    " AND Y.conditionId = W.conditionId " +
                    " AND X.targetQuestionId = W.targetQuestionId " +
                    " AND X.aliasTokenId = @aliasID " +
                    " and Y.conditionId = @condID " +
                    " and Y.serverityId = @severID ";
                SqlDataAdapter adapter = new SqlDataAdapter();
                command = new SqlCommand(sql, cnn);
                command.Parameters.AddWithValue("@aliasID", matchedAliasQuestionID);
                command.Parameters.AddWithValue("@condID", 1);  //cough condition
                command.Parameters.AddWithValue("@severID", 1); //mild severity
                SqlDataReader = command.ExecuteReader();
                while (SqlDataReader.Read())
                {
                    matchedVPAnswerText = (string)SqlDataReader[0];
                }
                SqlDataReader.Close();
                command.Dispose();
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Sorry, I could not understand your question. Can you please ask again.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, matchedVPAnswerText);
        }

        // POST api/values
        public void Post([FromBody] string value)
        {

        }

        //PUT api/values/5
        public HttpResponseMessage Put(string name, [FromBody] Client client)
        {
            try
            {
                using (DummyDBEntities entities = new DummyDBEntities())
                {
                    var entity = entities.Clients.FirstOrDefault(e => e.name.Equals(name));
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Client Not found " + name);
                    }
                    else
                    {
                        entity.confirmed = client.confirmed;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }
    }
}