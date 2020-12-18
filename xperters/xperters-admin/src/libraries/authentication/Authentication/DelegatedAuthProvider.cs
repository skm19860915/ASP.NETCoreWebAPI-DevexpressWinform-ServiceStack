using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Host;
using ServiceStack.Web;

namespace Xperters.Authentication
{
    public class DelegatedAuthProvider : NetCoreIdentityAuthProvider
    {
        /// <summary>
        /// Run custom filter after session created for In Process Requests
        /// </summary>
        public Action<IAuthSession, IRequest> PopulateInProcessSessionFilter { get; set; }

        public DelegatedAuthProvider(IAppSettings settings) : base(settings)
        {
		}

        public override void PreAuthenticate(IRequest req, IResponse res)
        {
            if (req is BasicRequest) // MQ or ServiceStack.Quartz jobs
            {
                CreateInProcessSession(req);
                return;
            }
            base.PreAuthenticate(req, res);
        }
        private void CreateInProcessSession(IRequest req)
        {
            var oid = Guid.NewGuid();
            var sessionId = $"key:asp:{oid}";

            var session = SessionFeature.CreateNewSession(req, sessionId).ConvertTo<AuthUserSession>();
            session.IsAuthenticated = true;
            session.Roles = new List<string> { "System" };
            session.AuthProvider = Name;
            session.Email = "noreply@Xperterscapital.com";
            session.LastName = "System";
            session.FirstName = "System";
            session.UserName = session.Email;
            session.DisplayName = "System";
            session.FromToken = true;

            PopulateInProcessSessionFilter?.Invoke(session, req);
            // the session must be passed onto the rest
            // of the ServiceStack plumbing
            req.Items[Keywords.Session] = session;
        }
    }
}