using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Auth
{
    public class AuthMethod : MonoBehaviour
    {
        protected bool _isSignedIn;
        protected bool _isError;
        protected AuthInfo _authInfo;

        public virtual string MethodName => string.Empty;
        public bool IsSignedIn => _isSignedIn;
        public bool IsError => _isError;
        public AuthInfo AuthInfo => _authInfo;

        public virtual string UserID => string.Empty;

        public virtual void Init()
        {

        }

        public virtual void SignIn()
        {

        }

        public virtual void SignOut()
        {

        }

    }
}
