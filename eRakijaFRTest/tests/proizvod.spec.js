const { test, expect } = require('@playwright/test');

test('Shop page loads products - OK', async ({ page }) => {

  await page.goto('http://localhost:4200/shop'); 


  await expect(page.locator('h2 >> text=Proizvodi')).toBeVisible();


  const productCount = await page.locator('.col-lg-3.p-3').count();
  expect(productCount).toBeGreaterThan(0);
});

test('Add new product through modal - OK', async ({ page }) => {

    await page.goto('http://localhost:4200/manageproducts');
  
    await page.getByRole('button', { name: 'Add Item', exact: true }).click();
  
    await page.waitForSelector('.modal', { visible: true });


    await page.getByPlaceholder('Naziv').fill('New Test Product');

    await page.getByPlaceholder('Cena').fill('100');

    await page.getByPlaceholder('Slika').fill('https://example.com/product-image.jpg');

    await page.getByPlaceholder('Opis').fill('This is a test product description.');

    await page.getByPlaceholder('Kolicina').fill('1');

    await page.getByPlaceholder('Tip Proizvoda Id').fill('1');
  

    await page.getByRole('button', { name: 'Add', exact: true }).click()
  
  });

  test('Edit product through modal - OK', async ({ page }) => {

    await page.goto('http://localhost:4200/manageproducts');
  

    await page.getByRole('button', { name: 'Edit item', exact: true }).click();

    await page.waitForSelector('.modal', { visible: true });
  

    await page.getByPlaceholder('Naziv').fill('Edited Test Product');

    await page.getByPlaceholder('Cena').fill('100');

    await page.getByPlaceholder('Slika').fill('https://example.com/product-image.jpg');

    await page.getByPlaceholder('Opis').fill('This is a test product description.');

    await page.getByPlaceholder('Kolicina').fill('1');

    await page.getByPlaceholder('Proizvod Id').fill('46');
  

    await page.getByRole('button', { name: 'Edit', exact: true }).click()

  });

  test('Delete product through modal - OK', async ({ page }) => {

    await page.goto('http://localhost:4200/manageproducts'); 
  

    await page.getByRole('button', { name: 'Delete item', exact: true }).click();
  

    await page.waitForSelector('.modal', { visible: true });
  

    await page.getByPlaceholder('Proizvod Id').fill('47');


    await page.getByRole('button', { name: 'Delete', exact: true }).click()
  
  });