<template>
	<div class="app">
		<RouterView v-slot="{ Component }">
			<Transition
				:key="$route.fullPath"
				enter-from-class="opacity-0"
				enter-to-class="opacity-100"
				leave-from-class="opacity-100"
				leave-to-class="opacity-0"
				mode="out-in"
			>
				<component :is="Component" />
			</Transition>
		</RouterView>
	</div>
</template>
