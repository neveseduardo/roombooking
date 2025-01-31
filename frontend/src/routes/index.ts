import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
	{
		path: '',
		redirect: 'home',
		component: () => import('@/layouts/dashboard.vue'),
		children: [
			{
				path: 'home',
				name: 'Home',
				component: () => import('@/pages/home.vue'),
			},
			{
				path: 'about',
				name: 'About',
				component: () => import('@/pages/about.vue'),
			},
		],
	},

	{
		path: '/:pathMatch(.*)',
		component: () => import('@/pages/notfound.vue'),
	},
];

const router = createRouter({
	history: createWebHistory(),
	routes,
});

router.beforeEach((_, __, next) => {
	return next();
});

export { router };
