
// this is for the http
const localApiUrl = 'http://localhost:5047';

// for the https 
// const localApiUrl = 'https://localhost:7225';



export const environment = {
  production: false,
  productsAPIURL: `${localApiUrl}/Product`,
  CategoryApiUrl: `${localApiUrl}/Category`,

  config: {
    env_name: 'dev',
    production: false,
    apiUrl: localApiUrl,
    apiEndpoints: {
      userProfile: 'user-profiles'
    },
    adb2cConfig: {
      clientId: 'c931645e-78a3-47aa-ac35-fdf05e1b3478',
      
      tenantId: '74372793-9c95-4100-9d21-9bf81968e4ed',
      
      authorityDomain: 'https://HeshamEcommerce.ciamlogin.com/74372793-9c95-4100-9d21-9bf81968e4ed',
      
      signUpUserFlow: 'SignUp_SignIn', 
      scopeUrls: [
        'https://HeshamEcommerce.onmicrosoft.com/Ecommerce/access_as_user'
      ],
      apiEndpointUrls: [ 
        localApiUrl 
      ]    },
    cacheTimeInMinutes: 30,
  }
};