(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('tableDialogController', ['$scope','$state','appCONSTANTS','$http','$translate' , 'TableResource','ToastService','$rootScope',  tableDialogController])

	function tableDialogController($scope, $state , appCONSTANTS,$http, $translate , TableResource,ToastService,$rootScope){
		var vm = this;
        vm.language = appCONSTANTS.supportedLanguage;
        
		vm.close = function(){
            $state.go('table');            
		}
		vm.isChanged = false;
		vm.AddNewTable = function(){
			vm.isChanged = true;
            var newTable = new TableResource();
            newTable.tableTitleDictionary = vm.tableTitleDictionary;
            newTable.tableAddressDictionary = vm.tableAddressDictionary;
            newTable.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('TableAddSuccess'),"success");
					 vm.isChanged = false;                     
                     $state.go('table');                     
                },
                function(data, status) {
                    vm.isChanged = false;                     
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
        }
        
	}	
}());
