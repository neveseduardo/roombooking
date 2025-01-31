import globals from "globals";
import pluginJs from "@eslint/js";
import tseslint from "typescript-eslint";
import pluginVue from "eslint-plugin-vue";

/** @type {import('eslint').Linter.Config[]} */
export default [
	{ files: ["**/*.{js,mjs,cjs,ts,vue}"] },
	{
		languageOptions: {
			globals: globals.browser,
		}
	},
	pluginJs.configs.recommended,
	...tseslint.configs.recommended,
	...pluginVue.configs["flat/strongly-recommended"],
	{
		files: [
			"**/*.vue",
			"**/*.ts",
		],
		languageOptions: {
			parserOptions: {
				parser: tseslint.parser
			}
		},
		rules: {
			quotes: ["error", "single"],
			semi: ["error", "always"],
			"quote-props": "off",

			"comma-dangle": ["error", {
				arrays: "always-multiline",
				objects: "always-multiline",
				imports: "always-multiline",
				exports: "always-multiline",
				functions: "never",
			}],
			"no-unreachable": "off",
			"no-useless-scape": "off",
			"no-control-regex": "off",
			"comma-spacing": ["error"],
			"no-var": "error",
			"no-undef": "off",
			"no-unused-vars": "off",
			"prefer-const": "error",
			eqeqeq: ["error", "smart"],
			"no-template-curly-in-string": "error",
			"no-duplicate-imports": "error",
			"default-param-last": ["error"],
			"array-element-newline": ["error", "consistent"],
			"arrow-spacing": ["error"],
			"block-spacing": ["error"],
			"brace-style": ["error"],
			"function-call-argument-newline": ["error", "consistent"],
			"jsx-quotes": ["error", "prefer-double"],

			"key-spacing": ["error", {
				mode: "strict",
			}],

			"keyword-spacing": ["error"],

			"no-multiple-empty-lines": ["error", {
				max: 1,
			}],

			"no-trailing-spaces": ["error", {
				ignoreComments: true,
			}],

			"no-whitespace-before-property": ["error"],

			"object-curly-newline": ["error", {
				consistent: true,
			}],

			"object-curly-spacing": ["error", "always"],
			"operator-linebreak": ["error", "after"],
			"rest-spread-spacing": ["error"],
			"space-before-blocks": ["error"],

			"space-before-function-paren": ["error", {
				anonymous: "never",
				named: "never",
				asyncArrow: "always",
			}],
			"@typescript-eslint/no-explicit-any": "off",

			"space-in-parens": ["error", "never"],
			"space-infix-ops": ["error"],
			"template-curly-spacing": ["error", "never"],
			"vue/no-side-effects-in-computed-properties": "off",
			"vue/multi-word-component-names": "off",
			"vue/component-definition-name-casing": ["error", "PascalCase"],

			"vue/component-name-in-template-casing": ["error", "PascalCase", {
				registeredComponentsOnly: false,
			}],

			"vue/prop-name-casing": [0],
			"vue/custom-event-name-casing": ["error", "kebab-case"],

			"no-tabs": "off",

			indent: ["error", "tab", {
				SwitchCase: 1,
				ignoredNodes: ["ConditionalExpression"],
				ignoreComments: true,
			}],

			"vue/html-indent": ["error", "tab", {
				attribute: 1,
				baseIndent: 1,
				closeBracket: 0,
				alignAttributesVertically: true,
				ignores: [],
			}],

			"vue/html-closing-bracket-newline": ["error", {
				singleline: "never",
				multiline: "always",
			}],

			"vue/script-indent": ["error", "tab", {
				switchCase: 1,
				ignores: ["//"],
			}],

			"no-constant-binary-expression": "off",

			"no-console": ["warn", {
				allow: ["error"],
			}],

			"no-debugger": ["error"],
		},
	},
];
