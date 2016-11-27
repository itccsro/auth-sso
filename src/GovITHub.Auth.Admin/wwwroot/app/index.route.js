(function () {
    'use strict';

    angular
      .module('authAdminPanel')
      .config(routerConfig);

    /** @ngInject */
    function routerConfig($stateProvider, $urlRouterProvider, $resourceProvider, $locationProvider) {
        $resourceProvider.defaults.stripTrailingSlashes = false;
        $urlRouterProvider.otherwise("/unauthorized");

        $stateProvider
        .state("authorized", { url: "/authorized", templateUrl: "/app/components/main/authorize.html", controller: "AuthorizeController" })
        .state("forbidden", { url: "/forbidden", templateUrl: "/templates/forbidden.html" })
        .state("unauthorized", { url: "/unauthorized", templateUrl: "/app/components/main/unauthorized.html" })
        .state("logoff", { url: "/logoff", templateUrl: "/app/components/main/logoff.html" , controller:"LogoffController"})
        .state('index', { abstract: true, url: "/index", templateUrl: "app/components/common/content.html" })
        .state('index.main', {
            url: "/main",
            templateUrl: "app/components/main/main.html",
            data: {
                pageTitle: 'Dashboard'
            }
        })
        .state('index.samples', {
            url: "/samples",
            controller: "SamplesListController as vm",
            templateUrl: "app/components/samples/list.html",
            data: {
                pageTitle: 'Samples'
            }
        })
        .state('index.samples_new', {
            url: "/samples/new",
            controller: "SamplesEditController as vm",
            templateUrl: "app/components/samples/edit.html",
            data: {
                pageTitle: 'Samples'
            },
            resolve: {
                status: [function () {
                    return { edit: false };
                }],
                id: [function () {
                    return null;
                }]
            }
        })
        .state('index.samples_edit', {
            url: "/samples/:id",
            controller: "SamplesEditController as vm",
            templateUrl: "app/components/samples/edit.html",
            data: {
                pageTitle: 'Samples'
            },
            resolve: {
                status: [function () {
                    return { edit: true };
                }],
                id: ['$stateParams', function ($stateParams) {
                    return $stateParams.id;
                }]
            }
        })
        .state('index.organizations', {
            url: "/organizations",
            controller: "OrganizationsListController as vm",
            templateUrl: "app/components/organizations/list.html",
            data: {
                pageTitle: 'Organizations'
            }
        })
        .state('index.organization_new', {
            url: "/organizations/new",
            controller: "OrganizationsEditController as vm",
            templateUrl: "app/components/organizations/edit.html",
            data: {
                pageTitle: 'Organizations'
            },
            resolve: {
                status: [function () {
                    return { edit: false };
                }],
                id: [function () {
                    return null;
                }]
            }
        })
        .state('index.organizations_edit', {
            url: "/organizations/:id",
            controller: "OrganizationsEditController as vm",
            templateUrl: "app/components/organizations/edit.html",
            data: {
                pageTitle: 'Organizations'
            },
            resolve: {
                status: [function () {
                    return { edit: true };
                }],
                id: ['$stateParams', function ($stateParams) {
                    return $stateParams.id;
                }]
            }
        })

          // -->Articles: pages
          .state('index.articles', {
              url: "/articles",
              controller: "articlesListController as vm",
              templateUrl: "app/components/articles/list.html",
              data: {
                  pageTitle: 'articles'
              }
          })
          //.state('index.articles_new/:articleId', {
          //    url: "/articles_new/:articleId",
          //    controller: "articlesNewController as vm",
          //    templateUrl: "app/components/articles/new.html",
          //    data: {
          //        pageTitle: 'articles'
          //    },
          //    resolve: {
          //        articleId: ['$stateParams', function ($stateParams) {
          //            return $stateParams.articleId;
          //        }]
          //    }
          //})
          //.state('index.articles_dashboard/:articleId', {
          //    url: "/articles_dashboard/:articleId",
          //    controller: "articlesDashboardController as vm",
          //    templateUrl: "app/components/articles/dashboard.html",
          //    data: {
          //        pageTitle: 'Products'
          //    },
          //    resolve: {
          //        articleId: ['$stateParams', function ($stateParams) {
          //            return $stateParams.articleId;
          //        }]
          //    }
          //})
          // -->Voting station: pages
          .state('index.voting_stations', {
              url: "/voting_stations",
              controller: "votingStationsListController as vm",
              templateUrl: "app/components/voting_stations/list.html",
              data: {
                  pageTitle: 'Lista sectii de votare'
              }
          })
          .state('index.voting_station_new', {
              url: "/voting_station_new",
              controller: "votingStationsNewController as vm",
              templateUrl: "app/components/voting_stations/new.html",
              data: {
                  pageTitle: 'Adaugare sectie de votare'
              }
          })
          .state('index.voting_station_update/:stationId', {
              url: "/voting_station_update/:stationId",
              controller: "votingStationsUpdateController as vm",
              templateUrl: "app/components/voting_stations/update.html",
              data: {
                  pageTitle: 'Modificare sectie de votare'
              },
              resolve: {
                  articleId: ['$stateParams', function ($stateParams) {
                      return $stateParams.stationId;
                  }]
              }
          })
          .state('index.voting_station_dashboard/:stationId', {
              url: "/voting_station_dashboard/:stationId",
              controller: "votingStationsDashboardController as vm",
              templateUrl: "app/components/voting_stations/dashboard.html",
              data: {
                  pageTitle: 'Detalii despre sectia de votare'
              },
              resolve: {
                  articleId: ['$stateParams', function ($stateParams) {
                      return $stateParams.stationId;
                  }]
              }
          })
          // -->User: pages
          .state('index.users', {
              url: "/users",
              templateUrl: "app/components/users/list.html",
              controller: "usersListController as vm",
              data: {
                  pageTitle: 'Users'
              }
          })
          .state('index.users_new', {
              url: "/users_new",
              templateUrl: "app/components/users/new.html",
              controller: "usersNewController as vm",
              data: {
                  pageTitle: 'New User'
              }
          })
          .state('index.users_dashboard/:userId', {
              url: "/users_dashboard/:userId",
              templateUrl: "app/components/users/dashboard.html",
              controller: "usersDashboardController as vm",
              data: {
                  pageTitle: 'UserName'
              },
              resolve: {
                  userId: ['$stateParams', function ($stateParams) {
                      return $stateParams.userId;
                  }]
              }
          });

        $locationProvider.html5Mode(true);
    }
})();
