import { defineStore } from 'pinia';

interface ExampleState {
	loading: boolean,
	collection: any,
}

export const useExampleStore = defineStore('ExampleStore', {
	state: (): ExampleState => ({
		loading: true,
		collection: null,
	}),
	getters: {},
	actions: {},
});
