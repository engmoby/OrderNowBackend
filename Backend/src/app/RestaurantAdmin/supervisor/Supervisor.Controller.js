(function () {
	'use strict';

	angular
		.module('home')
		.controller('SupervisorController', ['$scope', '$stateParams', '$translate', 'appCONSTANTS', '$uibModal', 'SupervisorResource', 'BranchResource', 'SupervisorsPrepService', 'ToastService', SupervisorController])

	function SupervisorController($scope, $stateParams, $translate, appCONSTANTS, $uibModal, SupervisorResource, BranchResource, SupervisorsPrepService, ToastService) {

		var vm = this;
		vm.Supervisors = SupervisorsPrepService;
		/*vm.SupervisorsLimit = SupervisorsLimitPrepService.SupervisorLimit;*/

		$('.pmd-sidebar-nav>li>a').removeClass("active")
		$($('.pmd-sidebar-nav').children()[2].children[0]).addClass("active")

		function refreshSupervisor() {
			var k = SupervisorResource.getAllSupervisors({ page: vm.currentPage }).$promise.then(function (results) {
				vm.Supervisors = results
			},
				function (data, status) {
					ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
				});
		}
		vm.currentPage = 1;
		vm.changePage = function (page) {
			vm.currentPage = page;
			refreshSupervisor();
		}
		vm.openSupervisorDialog = function () {
			var branches;
			var k = BranchResource.getAllBranchs({ areaId: 1 }).$promise.then(function (results) {
				branches = results;
				console.log(branches);
				var modalContent = $uibModal.open({
					templateUrl: './app/RestaurantAdmin/templates/newSupervisor.html',
					controller: 'SupervisorDialogController',
					controllerAs: 'SupervisorDlCtrl',
					resolve: {
						branches: function () { return branches.results; },
						callBackFunction: function () { return refreshSupervisor; },
						selectedLanguage: function () { return $scope.selectedLanguage; },
						CountriesPrepService: function () { return CountriesPrepService; }
					}

				});
			});
		}
		CountriesPrepService.$inject = ['CountryResource']
		function CountriesPrepService(CountryResource) {
			return CountryResource.getAllCountries({ pageSize: 0 }).$promise;
		}
		function confirmationDelete(itemId) {
			SupervisorResource.deleteSupervisor({ SupervisorId: itemId }).$promise.then(function (results) {
				ToastService.show("right", "bottom", "fadeInUp", $translate.instant('SupervisorDeleteSuccess'), "success");
				refreshSupervisor();
			},
				function (data, status) {
					ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
				});
		}
		vm.openDeleteSupervisorDialog = function (name, id) {
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
					itemName: function () { return name },
					itemId: function () { return id },
					message: function () { return null },
					callBackFunction: function () { return confirmationDelete }
				}

			});
		}

		vm.openEditSupervisorDialog = function (index) {
			var Supervisor;
			Supervisor = angular.copy(vm.Supervisors.results[index]);

			var branches;
			var k = BranchResource.getAllBranchs({ areaId: 1, pagesize: 0 }).$promise.then(function (results) {
				branches = results;

				var modalContent = $uibModal.open({
					templateUrl: './app/RestaurantAdmin/templates/editSupervisor.html',
					controller: 'editSupervisorDialogController2',
					controllerAs: 'editSupervisorDlCtrl',
					resolve: {
						mode: function () { return "edit" },
						Supervisor: function () { return Supervisor },
						branches: function () { return branches.results; },
						callBackFunction: function () { return refreshSupervisor; },
						selectedLanguage: function () { return $scope.selectedLanguage; }
					}

				});
			});

		}


	}

}
	());
