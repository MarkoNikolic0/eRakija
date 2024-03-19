const { test, expect } = require('@playwright/test');

test('All type of products types are displayed in dropdown', async ({ page }) => {

  await page.goto('http://localhost:4200/shop'); 


  await page.waitForSelector('#Filter');


  await page.getByRole('button', { name: 'Filtriraj proizvode po tipu' }).click();


  const productTypeElements = await page.$$('.dropdown-item');


  const expectedProductTypes = productTypeElements.length - 1;

  expect(expectedProductTypes).toBeGreaterThan(0);
});


test('Add new type of product through modal - OK', async ({ page }) => {

    await page.goto('http://localhost:4200/manageproducts');
  

    await page.getByRole('button', { name: 'Add Item Type' }).click();
  

    await page.waitForSelector('.modal', { visible: true });
  

    await page.getByPlaceholder('Naziv').fill('New Test Type of Product');
  

    await page.getByRole('button', { name: 'Add', exact: true }).click()
  
  });

  test('Edit type of product through modal - OK', async ({ page }) => {

    await page.goto('http://localhost:4200/manageproducts');
  

    await page.getByRole('button', { name: 'Edit item Type', exact: true }).click();


    await page.waitForSelector('.modal', { visible: true });
  

    await page.getByPlaceholder('Naziv').fill('Edited Test Product');

    await page.getByPlaceholder('Tip Id').fill('15');
  

    await page.getByRole('button', { name: 'Edit', exact: true }).click()
  
  });

  test('Delete type of product through modal - OK', async ({ page }) => {

    await page.goto('http://localhost:4200/manageproducts');
  

    await page.getByRole('button', { name: 'Delete item Type', exact: true }).click();


    await page.waitForSelector('.modal', { visible: true });
  

    await page.getByPlaceholder('Tip Id').fill('16');
  

    await page.getByRole('button', { name: 'Delete', exact: true }).click()
  
  });